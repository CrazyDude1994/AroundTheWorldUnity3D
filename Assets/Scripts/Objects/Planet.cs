using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    private float[] hills = new float[360];

	void Start ()
    {
        generateHeightMap(hills, 0, 359, 360, 0.1f);
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
	
	}
}
