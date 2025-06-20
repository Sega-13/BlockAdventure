using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameBlockAdv.SquareShape;

namespace GameBlockAdv.Grid
{
    public class GridSquare : MonoBehaviour
    {
        [SerializeField] private Image hooverImage;
        [SerializeField] private Image activeImage;
        [SerializeField] private Image normalImage;
        [SerializeField] private List<Sprite> normalImages;

        private SquareColor currentSquareColor = SquareColor.Notset;

        public SquareColor GetCurrentColor() { return currentSquareColor; }

        public bool Selected { get; set; }
        public int squareIndex { get; set; }
        public bool squareOccupied { get; set; }

        private IGridSquareState _availableState = new AvailableState();
        private IGridSquareState _occupiedState = new OccupiedState();
        private IGridSquareState _currentState;
        void Start()
        {
            Selected = false;
            squareOccupied = false;
            _currentState = _availableState;
        }

        public void PlaceShapeOnBoard(SquareColor color)
        {
            currentSquareColor = color;
            ActivateSquare();
        }
        public void ActivateSquare()
        {
            hooverImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(true);
            Selected = true;
            squareOccupied = true;
            _currentState = _occupiedState;
        }
        public void Deactivate()
        {
            currentSquareColor = SquareColor.Notset;
            activeImage.gameObject.SetActive(false);
        }
        public void ClearOccupied()
        {
            Selected = false;
            squareOccupied = false;
            _currentState = _availableState;
        }
        public void SetImage(bool setFirstImage)
        {
            normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _currentState.OnEnter(this, collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            _currentState.OnStay(this, collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _currentState.OnExit(this, collision);
        }
        public void ShowHoverImage(bool show)
        {
            hooverImage.gameObject.SetActive(show);
        }
    }

}
