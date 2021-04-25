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
    }
}
