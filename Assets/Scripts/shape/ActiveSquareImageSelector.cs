using UnityEngine;
using UnityEngine.UI;

namespace GameBlockAdv.SquareShape
{
    public class ActiveSquareImageSelector : MonoBehaviour
    {
        [SerializeField] private SquareTextureData squareTextureData;
        [SerializeField] private bool updateImageOnReacheadThreshold = false;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            UpdateSqColorBasedOnCurrentPt();
            if (updateImageOnReacheadThreshold)
            {
                GameEvents.UpdateSquareColor += UpdateSquareColor;
            }
        }
        private void OnDisable()
        {
            if (updateImageOnReacheadThreshold)
            {
                GameEvents.UpdateSquareColor -= UpdateSquareColor;
            }
        }

        private void UpdateSqColorBasedOnCurrentPt()
        {
            foreach (var squareTexture in squareTextureData.activeSquareTextures)
            {
                if (squareTextureData.currentColor == squareTexture.squareColor)
                {
                    image.sprite = squareTexture.texture;
                }

            }
        }

        private void UpdateSquareColor(SquareColor color)
        {
            foreach (var squareTexture in squareTextureData.activeSquareTextures)
            {
                if (color == squareTexture.squareColor)
                {
                    image.sprite = squareTexture.texture;
                }
            }
        }
    }
}

