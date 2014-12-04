﻿using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public float rougness = 0.1f;
    public float planetRadius = 100f;

    private float[] hills = new float[360];
    private Vector3[] vertices;
    private int[] triangles;

	void Start ()
    {
        generateHeightMap(hills, 0, 359, 360, rougness);

        Mesh mesh = new Mesh();
        vertices = new Vector3[hills.Length + 1];
        for (int i = 0; i < hills.Length; i++)
        {
            Vector2 position = getXYFromRadius(i, planetRadius, hills[i]);
            vertices[i] = new Vector3(position.x, position.y);
        }

        triangles = new int[vertices.Length * 3];
        for (int i = 0; i < triangles.Length - 100; i++)
        {
            triangles[i] = 0;
            triangles[i + 1] = i + 1;
            triangles[i + 2] = i + 2;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
	}

    Vector2 getXYFromRadius(float rotation, float radius, float offset)
    {
        float posX = (radius + offset) * Mathf.Sin(rotation * Mathf.Deg2Rad);
        float posY = (radius + offset) * Mathf.Cos(rotation * Mathf.Deg2Rad);
        return new Vector2(posX, posY);
    }

    void generateHeightMap(float[] vector, int left, int right, int len, float r)
    {
        if (right - left < 2)
        {
            return;
        }

        float hl = vector[left];
        float hr = vector[right];

        float h = (hl + hr) / 2 + Random.Range(-r * len, r * len);
        int index = Mathf.FloorToInt(left + (right - left) / 2);

        vector[index] = h;

        generateHeightMap(vector, left, index, len / 2, r);
        generateHeightMap(vector, index, right, len / 2, r);
    }
	
	void Update ()
    {
        for (int i = 0; i < vertices.Length - 1; i++)
        {
            Debug.DrawLine(gameObject.transform.position + vertices[i], gameObject.transform.position + vertices[i + 1]);
        }
	}
}
