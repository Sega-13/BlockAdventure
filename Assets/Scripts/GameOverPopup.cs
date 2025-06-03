using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public GameObject gameOverPopup;
    public GameObject loosPopup;
    public GameObject newBestScorePopup;

    void Start()
    {
        gameOverPopup.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.GameOver += GameOver;
    }
    private void OnDisable()
    {
        GameEvents.GameOver -= GameOver;
    }
    private void GameOver(bool newBestScore)
    {
        gameOverPopup.SetActive(true);
        loosPopup.SetActive(false);
        newBestScorePopup.SetActive(true) ;
    }
}
