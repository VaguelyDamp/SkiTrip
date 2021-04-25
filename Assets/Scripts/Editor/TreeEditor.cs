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

        if (GUILayout.Button("Change Spacing"))
        {
            foreach (Tree thing in targets)
            {
                thing.ChangeSpacing();
            }
        }

        if (GUILayout.Button("Randomize Rotation"))
        {
            foreach (Tree thing in targets)
            {
                thing.RandomizeRotation();
            }
        }

        if (GUILayout.Button("Randomize Variant"))
        {
            foreach (Tree thing in targets)
            {
                GameObject newTreePrefab = thing.variants[(int)Random.Range(0, thing.variants.Length)];
                object newTreeActual = PrefabUtility.InstantiatePrefab(newTreePrefab, thing.transform.parent.transform);
                GameObject go = newTreeActual as GameObject;
                go.transform.position = thing.transform.position;
                go.transform.rotation = thing.transform.rotation;
                Undo.DestroyObjectImmediate(thing.gameObject);
            }
        }
    }
}
