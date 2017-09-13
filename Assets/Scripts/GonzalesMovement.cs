using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzalesMovement : MonoBehaviour
{
    public float moveSpeed;
    
	// Use this for initialization

	
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
    }
}
