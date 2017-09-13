using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform gonzales;

    public float zoomLevel = 5;
    public float smoothTime = 1f;

    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = zoomLevel;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, gonzales.position.x, Time.deltaTime / smoothTime), Mathf.Lerp(transform.position.y, gonzales.position.y, Time.deltaTime / smoothTime), -10);
	}
}
