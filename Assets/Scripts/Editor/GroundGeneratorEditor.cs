using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroundGenerator))]
public class GroundGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            GroundGenerator theBigG = (GroundGenerator)target;
            theBigG.Generate();
        }
    }
}
