using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ProceduralGround[] grounds;

    #region Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance;

    public GonzalesMovement gonzales;
    public FireWave fireWave;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Text gameOverText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gonzales.transform.position.y <= -10)
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        fireWave.StopFireWave();
        StartCoroutine(DisplayGameOverText());
        foreach (ProceduralGround ground in grounds)
        {
            ground.ResetLevel();
        }
        gonzales.transform.position = new Vector2(grounds[0].GetTileLeftX(), 10);
    }

    public void ShowGameOverText(bool show)
    {
        gameOverText.gameObject.SetActive(show);
    }

    IEnumerator DisplayGameOverText()
    {
        ShowGameOverText(true);
        yield return new WaitForSeconds(1f);
        ShowGameOverText(false);
    }
}
