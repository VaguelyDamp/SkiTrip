using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject treeButt;

    public int oldSpacing;
    public int newSpacing;

    public void DropTree()
    {
        int layerMask = 1 << 6;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Found ground, Treebottem transform: " + treeButt.transform.position);
            transform.position = new Vector3(transform.position.x, hit.point.y - (treeButt.transform.localPosition.y * transform.localScale.y), transform.position.z);
        }
    }


    public void ChangeSpacing()
    {
        int spacingModifier = newSpacing / oldSpacing;
        Debug.Log("Spacing Modifier: " + spacingModifier);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z * (spacingModifier));
    }
}