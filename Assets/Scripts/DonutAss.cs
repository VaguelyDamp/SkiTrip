using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonutAss : MonoBehaviour
{
    public float Fill
    {
        get
        {
            return curT_;
        }
        set 
        {
            curT_ = value;
            progressBarUI_.fillAmount = curT_;
        }
    }
    
    private float curT_;

    public Image progressBarUI_;
}
