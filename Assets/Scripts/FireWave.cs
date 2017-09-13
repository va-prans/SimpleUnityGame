using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.EmissionModule psEmission;

    bool left;
    float speed;

    public bool isActive;

	// Use this for initialization
	void Start ()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        psMain = ps.main;
        psEmission = ps.emission;

        speed = 5;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (isActive)
        {
            if (left)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
    }

    public void StartFireWave(bool _left, Vector2 startPos, float _speed)
    {
        left = _left;
        speed = _speed;

        transform.position = startPos;

        if (_left)
        {
            psMain.startRotation = 90;
            psMain.startSpeed = 1;
        }
        else
        {
            psMain.startRotation = -90;
            psMain.startSpeed = -1;
        }

        isActive = true;
        psEmission.enabled = true;
    }

    public void StopFireWave()
    {
        isActive = false;
        psEmission.enabled = false;
    }

}
