using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public Vector2 force;

	void Start ()
    {

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            force += Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            force += -Vector2.up;
        }
    }

    void FixedUpdate()
    {
        gameObject.rigidbody2D.AddRelativeForce(force);
    }
}
