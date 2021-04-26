using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairliftSway : MonoBehaviour
{
    public float swayAmount = 3;
    public float swaySpeed = .2f;

    private float sway = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sway >=0)
        {
            sway = Mathf.MoveTowardsAngle(sway, -swayAmount, swaySpeed);
        }
        else
        {
            sway = Mathf.MoveTowardsAngle(sway, swayAmount, swaySpeed);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, sway*Time.deltaTime);
    }
}
