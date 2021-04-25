using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject treeButt;

    public float oldSpacing;
    public float newSpacing;

    public bool randomizeRotation = true;
    public bool randomizeVariant = true;

    public GameObject[] variants;

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
        float spacingModifier = newSpacing / oldSpacing;
        Debug.Log("Spacing Modifier: " + spacingModifier);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z * (spacingModifier));
    }

    public void RandomizeRotation()
    {
        if (randomizeRotation)
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }

    public void RandomizeVariant()
    {
        GameObject newTreePrefab = variants[(int)Random.Range(0, variants.Length - 1)];
        GameObject newTreeActual = Instantiate(newTreePrefab, transform.position, transform.rotation, transform.parent);
        Destroy(this);
    }
}
