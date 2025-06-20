using UnityEngine;

namespace GameBlockAdv.Grid
{
    public class AvailableState : IGridSquareState
    {
        public void OnEnter(GridSquare square, Collider2D collision)
        {
            square.Selected = true;
            square.ShowHoverImage(true);
        }

        public void OnStay(GridSquare square, Collider2D collision)
        {
            square.Selected = true;
            square.ShowHoverImage(true);
        }

        public void OnExit(GridSquare square, Collider2D collision)
        {
            square.Selected = false;
            square.ShowHoverImage(false);
        }
    }
}

