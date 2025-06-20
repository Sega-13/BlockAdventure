using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBlockAdv.Score
{
    public class BestScoreBar : MonoBehaviour
    {
        [SerializeField] private Image fillInImage;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        private void OnEnable()
        {
            GameEvents.UpdateBestScoreBar += UpdateBestScoreBar;
        }
        private void OnDisable()
        {
            GameEvents.UpdateBestScoreBar -= UpdateBestScoreBar;
        }

        public void UpdateBestScoreBar(int currentScore, int bestScore)
        {
            float currentPercentage = (float)currentScore / (float)bestScore;
            fillInImage.fillAmount = currentPercentage;
            bestScoreText.text = bestScore.ToString();
        }

    }
}

