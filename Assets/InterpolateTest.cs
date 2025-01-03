using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateTest : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i < 360; i++)
        {
            Debug.DrawLine(getXYFromRadius(i, 100, 1), getXYFromRadius(i + 1, 100, 0));
        }    
    }
    
    Vector2 getXYFromRadius(float rotation, float radius, float t)
    {
        float posX = radius * Mathf.Sin(rotation * Mathf.Deg2Rad) * t;
        float posY = radius * Mathf.Cos(rotation * Mathf.Deg2Rad) * t;
        return new Vector2(posX, posY);
    }
}
