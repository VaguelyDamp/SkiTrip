using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGroup : MonoBehaviour
{
    public void DropTrees()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "TreeGroup")
            {
                child.GetComponent<TreeGroup>().DropTrees();
            }
            else if (child.tag == "Tree")
            {
                child.GetComponent<Tree>().DropTree();
            }
            else
            {
                Debug.LogWarning("There's an imposter in: " + gameObject.name + "!!!!!!!!"); 
            }
        }
    }
}
