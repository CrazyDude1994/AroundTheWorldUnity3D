using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MidPointGeneratorTest : MonoBehaviour
{
    public int resolution = 8;
    public int len = 8;
    public float[] heights;
    public float roughness = 0.5f;
    public GameObject[] terrain;
    public GameObject cubePrefab;
    
    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (var i = 0; i < terrain.Length; i++)
        {
            DestroyImmediate(terrain[i]);
        }
        heights = new float[resolution];
        terrain = new GameObject[resolution];
        Random.InitState(0);
        generateHeightMap(heights, 0, resolution - 1, resolution, roughness);
        for (var i = 0; i < heights.Length; i++)
        {
            terrain[i] = Instantiate(cubePrefab, new Vector3(i, -heights[i] / 2, 0), Quaternion.identity);
            terrain[i].transform.localScale = new Vector3(1, heights[i], 1);
        }
    }

    private void OnValidate()
    {
        // Generate();
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


    void Update()
    {
        var step = (float)len / resolution;
        for (var i = 0; i < resolution; i++)
        {
            Debug.DrawLine(new Vector3(i * step, -heights[i]), new Vector3(i * step + step, -heights[i + 1]), Color.red);
        }
    }
}