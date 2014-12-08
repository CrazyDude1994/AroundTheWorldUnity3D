using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

    public float rougness = 0.1f;
    public float planetRadius = 100f;
    public bool isDebug = true;
    public int mass = 10000;
    public int planetRandomSeed = 0;

    private float[] hills = new float[360];
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private Vector2[] collisionPoints = new Vector2[360];
    private Vector2[] uv;

	void Start ()
    {
        Random.seed = planetRandomSeed;
        generateHeightMap(hills, 0, 359, 360, rougness);

        Mesh mesh = new Mesh();
        vertices.Add(Vector3.zero);
        for (int i = 0; i < hills.Length; i++)
        {
            Vector2 position = getXYFromRadius(i, planetRadius, hills[i]);
            vertices.Add(new Vector3(position.x, position.y));
            collisionPoints[i] = position;
        }

        for (int i = 0; i < vertices.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }
        triangles.Add(0);
        triangles.Add(vertices.Count - 1);
        triangles.Add(1);
        uv = new Vector2[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            uv[i] = new Vector2(0.5f + (vertices[i].x) / (2 * planetRadius),
                                0.5f + (vertices[i].y) / (2 * planetRadius));
        }
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.name = "PlanetMesh";
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.SetPath(0, collisionPoints);
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
        if (isDebug)
        {
            for (int i = 1; i < vertices.Count - 1; i++)
            {
                Debug.DrawLine(gameObject.transform.position + vertices[i], gameObject.transform.position + vertices[i + 1]);
            }
            Debug.DrawLine(gameObject.transform.position + vertices[1], gameObject.transform.position + vertices[vertices.Count - 1]); //skip drawing last vertex
        }
	}


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, planetRadius);
    }
}
