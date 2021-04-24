using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tree)), CanEditMultipleObjects]
public class TreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Drop Tree"))
        {
            foreach(Tree thing in targets)
            {
                thing.DropTree();
            }
        }
    }
}
