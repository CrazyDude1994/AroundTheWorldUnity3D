using UnityEngine;
using System.Collections;

public class PhysicObject : MonoBehaviour {

    public Planet currentPlanet;

	void Start ()
    {
        //gameObject.rigidbody2D.AddRelativeForce(Vector2.up * 30, ForceMode2D.Impulse);
	}

    void FixedUpdate()
    {
        if (currentPlanet)
        {
            Vector2 position = gameObject.transform.position;
            Vector2 planetPosition = currentPlanet.transform.position;
            Vector2 diff = planetPosition - position;
            Vector2 force = diff.normalized;
            Vector3 height = gameObject.transform.position - currentPlanet.transform.position;
            float g = 6.67428f * (currentPlanet.mass / Mathf.Pow(height.magnitude, 2));
            g *= currentPlanet.gravityMultiplier;
            gameObject.rigidbody2D.AddForce(force * (g * Time.deltaTime) * gameObject.rigidbody2D.mass, ForceMode2D.Force);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Planet")
        {
            currentPlanet = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Planet")
        {
            currentPlanet = collider.gameObject.GetComponent<Planet>();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, gameObject.rigidbody2D.velocity + gameObject.rigidbody2D.position); //draw velocity vector
    }
}
