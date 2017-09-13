using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzalesMovement : MonoBehaviour
{
    public float moveSpeed;

    Vector2 startPosition;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetMouseButton(0))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButton(1))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }

        if (transform.position.y < -10)
        {
            transform.position = startPosition;
        }
    }
}
