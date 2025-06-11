using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareFactory 
{
    private readonly GameObject _gridSquarePrefab;

    public GridSquareFactory(GameObject gridSquarePrefab)
    {
        _gridSquarePrefab = gridSquarePrefab;
    }

    public GameObject Create(int index, Transform parent, float scale, bool isEven)
    {
        GameObject square = Object.Instantiate(_gridSquarePrefab, Vector3.zero, Quaternion.identity, parent);
        square.transform.localScale = new Vector3(scale, scale, scale);

        GridSquare gridSquare = square.GetComponent<GridSquare>();
        gridSquare.squareIndex = index;
        gridSquare.SetImage(isEven);

        return square;
    }
}
