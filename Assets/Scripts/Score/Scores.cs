using System.Collections;
using TMPro;
using UnityEngine;
using GameBlockAdv.SquareShape;
using GameBlockAdv.Utility;

namespace GameBlockAdv.Score
{
    [System.Serializable]
    public class BestScoreData
    {
        public int score = 0;
    }
    public class Scores : MonoBehaviour
    {
        [SerializeField] private SquareTextureData squareTextureData;
        [SerializeField] private TextMeshProUGUI scoreText;

        private bool _newBestScore = false;
        private BestScoreData _bestScoreData = new BestScoreData();
        private int _currentScores;
        private string _binaryScoreKey = "bsdat";

        private void Awake()
        {
            if (BinaryDataStream.Exist(_binaryScoreKey))
            {
                StartCoroutine(ReadDataFile());
            }

        }
        private IEnumerator ReadDataFile()
        {
            _bestScoreData = BinaryDataStream.Read<BestScoreData>(_binaryScoreKey);
            if (_bestScoreData == null)
            {
                Debug.LogError("Best score data is null! Possibly missing or corrupted file.");
                yield break;
            }
            yield return new WaitForEndOfFrame();
            GameEvents.UpdateBestScoreBar(_currentScores, _bestScoreData.score);
            Debug.Log("Read Best Score = " + _bestScoreData.score);
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
            if (_currentScores > _bestScoreData.score)
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
            if (GameEvents.UpdateSquareColor != null && _currentScores >= squareTextureData.thresholdVal)
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
}

