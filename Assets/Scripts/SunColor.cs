using UnityEngine;

class SunColor : MonoBehaviour
{
    public SunColor (Color color, float progressTrigger)
    {
        this.color = color;
        this.progressTrigger = progressTrigger;
    }

    public Color color;
    public float progressTrigger;
}

