using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float rotateSpeed = 60;

    public float amplitude = .5f;
    public float frequency = 1f;

    public GameObject insidePiece;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        float tempY = tempPos.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        tempPos = new Vector3(transform.position.x, tempY, transform.position.z);
        transform.position = tempPos;

        insidePiece.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }
}
