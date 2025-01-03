using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SphereMeshGenerator : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        generateMesh();
    }

    private void generateMesh()
    {
        var mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = new[]
            { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        mesh.triangles = new[] { 0, 1, 2, 2, 0, 3 };
        mesh.RecalculateNormals();
        mesh.uv = new[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
    }
}