using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

[System.Serializable]
public class BestScoreData
{
    public int score = 0;
}
public class Scores : MonoBehaviour
{
    public SquareTextureData squareTextureData;
    public TextMeshProUGUI scoreText;
    private bool _newBestScore = false;
    private BestScoreData _bestScoreData = new BestScoreData();
    private int _currentScores;
    private string _binaryScoreKey = "bsdat";

    private void Awake()
    {
        if(BinaryDataStream.Exist(_binaryScoreKey))
        {
            StartCoroutine(ReadDataFile());
        }
        
    }
    private IEnumerator ReadDataFile()
    {
        _bestScoreData = BinaryDataStream.Read<BestScoreData>(_binaryScoreKey);
        yield return new WaitForEndOfFrame();
        GameEvents.UpdateBestScoreBar(_currentScores, _bestScoreData.score);
        Debug.Log("Read Best Score = "+ _bestScoreData.score);
    }
    void Start()
    {
        _currentScores = 0;
        _newBestScore = false;
        squareTextureData.SetStartColor();
        UpdateScoreText();
    }
    private void OnEnable()
    {
        GameEvents.AddScores += AddScores;
        GameEvents.GameOver += SaveBestScores;
    }
    private void OnDisable()
    {
        GameEvents.AddScores -= AddScores;
        GameEvents.GameOver -= SaveBestScores;
    }

    private void AddScores(int scores)
    {
        _currentScores += scores;
        if(_currentScores > _bestScoreData.score)
        {
            _newBestScore = true;
            _bestScoreData.score = _currentScores;
            SaveBestScores(true);
        }
        UpdateSquareColor();
        GameEvents.UpdateBestScoreBar(_currentScores, _bestScoreData.score);
        UpdateScoreText();
    }
    private void UpdateSquareColor()
    {
        if(GameEvents.UpdateSquareColor != null && _currentScores >= squareTextureData.thresholdVal)
        {
            squareTextureData.UpdateColor(_currentScores);
            GameEvents.UpdateSquareColor(squareTextureData.currentColor);
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = _currentScores.ToString();
    }
    public void SaveBestScores(bool newBestScore)
    {
        BinaryDataStream.Save<BestScoreData>(_bestScoreData, _binaryScoreKey);
    }
}
