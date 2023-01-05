#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CustomEditor(typeof(Shop_SO))]
public class Shop_SO_Inspector : Editor
{
    string id = "";
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Random ID");
        id = EditorGUILayout.TextField(id);
        if (GUILayout.Button("Randomize ID")) RandomizeID();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        DrawDefaultInspector();
    }

    void RandomizeID()
    {
        id = System.Guid.NewGuid().ToString();
    }
}

#endif
