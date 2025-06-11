using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedState : IGridSquareState
{
    public void OnEnter(GridSquare square, Collider2D collision)
    {
        NotifyShape(square, collision);
    }

    public void OnStay(GridSquare square, Collider2D collision)
    {
        NotifyShape(square, collision);
    }

    public void OnExit(GridSquare square, Collider2D collision)
    {
        UnnotifyShape(square, collision);
    }

    private void NotifyShape(GridSquare square, Collider2D collision)
    {
        var shape = collision.GetComponent<ShapeSquare>();
        shape?.SetOccupied();
    }

    private void UnnotifyShape(GridSquare square, Collider2D collision)
    {
        var shape = collision.GetComponent<ShapeSquare>();
        shape?.UnSetOccupied();
    }
}
