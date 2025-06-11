using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridSquareState 
{
    void OnEnter(GridSquare square, Collider2D collision);
    void OnStay(GridSquare square, Collider2D collision);
    void OnExit(GridSquare square, Collider2D collision);
}
