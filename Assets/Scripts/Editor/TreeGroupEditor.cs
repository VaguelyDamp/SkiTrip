using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeGroup)), CanEditMultipleObjects]
public class TreeGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Drop Tree"))
        {
            foreach (TreeGroup thing in targets)
            {
                thing.DropTrees();
            }
        }

        if (GUILayout.Button("Randomize Rotations"))
        {
            foreach (TreeGroup thing in targets)
            {
                thing.RotateTrees();
            }
        }


        //NO NO BUTTON
        /*if (GUILayout.Button("Randomize Variants"))
        {
            foreach (TreeGroup thing in targets)
            {
                foreach (Transform child in thing.gameObject.transform)
                {
                    if (child.tag == "Tree")
                    {
                        Tree childTree = child.GetComponent<Tree>();
                        GameObject newTreePrefab = childTree.variants[(int)Random.Range(0, childTree.variants.Length)];
                        object newTreeActual = PrefabUtility.InstantiatePrefab(newTreePrefab, thing.transform);
                        GameObject go = newTreeActual as GameObject;
                        go.transform.position = thing.transform.position;
                        go.transform.rotation = thing.transform.rotation;
                        Undo.DestroyObjectImmediate(child);
                    }
                }
            }
        }*/
    }
}
