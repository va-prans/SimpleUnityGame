using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer sprite;

	// Use this for initialization
	void Awake ()
    {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetSpriteSize()
    {
        return sprite.size.x - 0.14f;
    }
}
