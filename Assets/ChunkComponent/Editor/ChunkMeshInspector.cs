using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkRenderer))]
public class ChunkMeshInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Rebuild mesh"))
        {
            (target as ChunkRenderer).RegenerateMesh();
        }
        
        if (GUILayout.Button("Generate"))
        {
            (target as ChunkRenderer).RegenerateMesh();
        }
    }
}