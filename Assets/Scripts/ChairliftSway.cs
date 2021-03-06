using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairliftSway : MonoBehaviour
{
    public float swayAmount = 3;
    private float maxSway;
    public float swaySpeed = .2f;

    private float sway = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxSway = swayAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sway >= swayAmount)
        {
            maxSway = -swayAmount;
            Debug.Log("Max Sway set to: "+maxSway);
        } 
        else if (sway <= -swayAmount)
        {
            maxSway = swayAmount;
            Debug.Log("Max Sway set to: "+maxSway);
        } 

        sway = Mathf.MoveTowardsAngle(sway, maxSway, swaySpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, sway);
    }
}
