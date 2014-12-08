using UnityEngine;
using System.Collections;

public class PhysicObject : MonoBehaviour {

    public Planet currentPlanet;

	void Start ()
    {
        gameObject.rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
	}
	
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        if (currentPlanet)
        {
            Vector2 position = gameObject.transform.position;
            Vector2 planetPosition = currentPlanet.transform.position;
            Vector2 diff = planetPosition - position;
            Vector2 force = diff.normalized;
            float g = 6.67428f * (currentPlanet.mass / Mathf.Pow(currentPlanet.planetRadius, 2));
            Debug.Log(g);
            gameObject.rigidbody2D.AddForce(force * (g * Time.deltaTime) * gameObject.rigidbody2D.mass, ForceMode2D.Force);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, gameObject.rigidbody2D.velocity + gameObject.rigidbody2D.position);
    }
}
