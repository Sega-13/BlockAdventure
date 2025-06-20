using UnityEngine;

namespace GameBlockAdv.GameMenu
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPopup;
        [SerializeField] private GameObject loosPopup;
        [SerializeField] private GameObject newBestScorePopup;

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
            newBestScorePopup.SetActive(true);
        }
    }
}

