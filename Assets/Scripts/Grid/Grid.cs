using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public ShapeStorage shapeStorage;
    public int columns = 0;
    public int rows = 0;
    public float squareGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffeset = 0.0f;
    public SquareTextureData squareTextureData;

    private Vector2 _offset = new Vector2(0.0f,0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    private LineIndicator _lineIndicator;
    private Config.SquareColor currentActiveSquareColor = Config.SquareColor.Notset;
    private List<Config.SquareColor> colorsInGrid = new List<Config.SquareColor>();
    private void OnEnable()
    {
        GameEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
        GameEvents.UpdateSquareColor += OnUpdateSquareColor;
        GameEvents.CheckIfPlayerLost += CheckIfPlayerLost;
    }
    private void OnDisable()
    {
        GameEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
        GameEvents.UpdateSquareColor -= OnUpdateSquareColor;
        GameEvents.CheckIfPlayerLost -= CheckIfPlayerLost;
    }
   
    void Start()
    {
        _lineIndicator = GetComponent<LineIndicator>();
        CreateGrid();
        currentActiveSquareColor = squareTextureData.activeSquareTextures[0].squareColor;
    }
    private void OnUpdateSquareColor(Config.SquareColor color)
    {
        currentActiveSquareColor = color;
    }
    private List<Config.SquareColor> GetAllColorsInGrid()
    {
        var colors = new List<Config.SquareColor>();
        foreach (var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();
            if(gridSquare.squareOccupied)
            {
                var color = gridSquare.GetCurrentColor();
                if (colors.Contains(color) == false)
                {
                    colors.Add(color);
                }
            }
        }
        return colors;
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPositions();
    } 
    private void SpawnGridSquares()
    {
        int square_index = 0;
        var factory = new GridSquareFactory(gridSquare);

        for (var row = 0; row < rows; ++row)
        {
            for (var col = 0; col < columns; ++col)
            {
                bool isEven = _lineIndicator.GetGridSquareIndex(square_index) % 2 == 0;
                GameObject square = factory.Create(square_index, this.transform, squareScale, isEven);
                _gridSquares.Add(square);
                square_index++;
            }
        }
        /*int square_index = 0;
        for(var row  = 0; row < rows; ++row)
        {
            for (var col = 0; col < columns; ++col)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().squareIndex = square_index;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetImage(_lineIndicator.GetGridSquareIndex(square_index) % 2 == 0);
                square_index++;
            }
        }*/
    }
    private void SetGridSquaresPositions()
    {
        int row_number = 0;
        int col_number = 0;
        Vector2 square_gap_no = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        var square_rect = _gridSquares[0].GetComponent<RectTransform>();
        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffeset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffeset;
        foreach(GameObject square  in _gridSquares)
        {
            if(col_number + 1 > columns)
            {
                square_gap_no.x = 0;
                col_number = 0;
                row_number++;
                row_moved = false;
            }
            var pos_x_offset = _offset.x * col_number + (square_gap_no.x * squareGap);
            var pos_y_offset = _offset.y * row_number + (square_gap_no.y * squareGap);
            if(col_number > 0 && col_number%3 == 0)
            {
                square_gap_no.x++;
                pos_x_offset += squareGap;
            }
            if(row_number > 0 && row_number%3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_no.y++;
                pos_y_offset += squareGap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset, 0.0f);
            col_number++;
        }

    }
    private void CheckIfShapeCanBePlaced()
    {
        var squareIndexs = new List<int>();
        foreach(var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();
            if(gridSquare.Selected && !gridSquare.squareOccupied )
            {
                squareIndexs.Add(gridSquare.squareIndex);
                gridSquare.Selected = false;
                //gridSquare.ActivateSquare();
            }
        }
        var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
        if (currentSelectedShape == null) return;
        if(currentSelectedShape.TotalSquareNumber == squareIndexs.Count)
        {
            foreach(var squareIndex  in squareIndexs)
            {
                _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard(currentActiveSquareColor);
            }
            var shapeLeft = 0;
            foreach(var shape in shapeStorage.shapeList)
            {
                if(shape.IsOnStartPosition() && shape.IsOfAnyShapeSquareActive())
                {
                    shapeLeft++;
                }
            }
           // currentSelectedShape.DeactivateShape();
            if(shapeLeft == 0)
            {
                GameEvents.RequestNewShapes();
            }
            else
            {
                GameEvents.SetShapeInactive();
            }
            //CheckIfAnyLineIsCompleted();
        }
        else
        {
            GameEvents.MoveShapeToStartPosition();
        }
        CheckIfAnyLineIsCompleted();
        //shapeStorage.GetCurrentSelectedShape().DeactivateShape();
    }

    void CheckIfAnyLineIsCompleted()
    {
        List<int[]> lines = new List<int[]>();
        //columns
        foreach(var column in _lineIndicator.columnIndexs)
        {
            lines.Add(_lineIndicator.GetVerticalLine(column));
        }
        //rows
        for(int row = 0; row < 9; row++)
        {
            List<int> data = new List<int>(9);
            for (var index = 0; index < 9; index++)
            {
                data.Add(_lineIndicator.line_data[row, index]);
            }
            lines.Add(data.ToArray());
        }
        //Squares
        for(var square = 0; square<9; square++)
        {
            List<int> data = new List<int>(9);
            for(int index = 0;index < 9; index++)
            {
                data.Add(_lineIndicator.square_data[square, index]);
            }
            lines.Add(data.ToArray());
        }
        //function need to called before CheckIfSquaresAreCompleted
        colorsInGrid = GetAllColorsInGrid();

        var completedLines = CheckIfSquaresAreCompleted(lines);
        if(completedLines >= 2)
        {
            GameEvents.ShowCongratulations();
        }
        var totalScores = 10 * completedLines;
        var bonusScores = ShouldPlayColorBonusAnim();
        GameEvents.AddScores(totalScores+ bonusScores);
        GameEvents.CheckIfPlayerLost();
        //CheckIfPlayerLost();
    }
    private int ShouldPlayColorBonusAnim()
    {
        var colorsInGridAfterLineRemoved = GetAllColorsInGrid();
        Config.SquareColor colorToPlayBonus = Config.SquareColor.Notset;
        foreach(var squareColor in colorsInGrid)
        {
            if(colorsInGridAfterLineRemoved.Contains(squareColor) == false)
            {
                colorToPlayBonus = squareColor;
            }
        }
        if(colorToPlayBonus == Config.SquareColor.Notset)
        {
            return 0;
        }
        //never play bonus for current color
        if(colorToPlayBonus == currentActiveSquareColor)
        {
            return 0;
        }
        GameEvents.ShowBonusScreen(colorToPlayBonus);
        return 50;
    }
    private int CheckIfSquaresAreCompleted(List<int[]> data)
    {
        List<int[]> completedLines = new List<int[]>();
        var linesCompleted = 0;
        foreach(var line in data)
        {
            var lineCompleted = true;
            foreach(var squareIndex  in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                if(comp.squareOccupied == false)
                {
                    lineCompleted = false;

                }
            }
            if(lineCompleted)
            {
                completedLines.Add(line);
            }
        }
        foreach(var line in completedLines)
        {
            var completed = false;
            foreach(var squareIndex in line)
            {
                var com = _gridSquares[squareIndex].GetComponent<GridSquare>();
                com.Deactivate();
                completed = true;
            }
            foreach (var squareIndex in line)
            {
                var com = _gridSquares[squareIndex].GetComponent<GridSquare>();
                com.ClearOccupied();
            }
            if (completed)
            {
                linesCompleted++;
            }
        }
        return linesCompleted;
    }
    private void CheckIfPlayerLost()
    {
        var validShapes = 0;
        for(int index = 0; index<shapeStorage.shapeList.Count; index++)
        {
            var isShapeActive = shapeStorage.shapeList[index].IsOfAnyShapeSquareActive();
            if (CheckIfShapeCanBePlacedOnGrid(shapeStorage.shapeList[index]) && isShapeActive)
            {
                shapeStorage.shapeList[index]?.ActivateShape();
                validShapes++;
            }

        }
        if(validShapes == 0)
        {
            GameEvents.GameOver(false);
            Debug.Log("Game Over");
        }
    }
    private bool CheckIfShapeCanBePlacedOnGrid(Shape currentShape)
    {
        var currentShapeData = currentShape.CurrentShapeData;
        var shapeColumns = currentShapeData.columns;
        var shapeRows = currentShapeData.rows;
        List<int> originalShapeFilledUpSquares = new List<int>();
        var squreIndex = 0;
        for(int rowIndex =0; rowIndex<shapeRows;  rowIndex++)
        {
            for(int columnIndex =0; columnIndex<shapeColumns; columnIndex++)
            {
                if (currentShapeData.board[rowIndex].column[columnIndex])
                {
                    originalShapeFilledUpSquares.Add(squreIndex);
                }
                squreIndex++;
            }
        }
        if(currentShape.TotalSquareNumber != originalShapeFilledUpSquares.Count)
        {
            Debug.Log("End");
        }
        var squareList = GetAllSquaresCombination(shapeColumns,shapeRows);
        bool canBePlaced = false;
        foreach(var number in squareList)
        {
            bool shapeCanBePlacedOnBoard = true;
            foreach(var squareIndexToCheck in originalShapeFilledUpSquares)
            {
                var comp = _gridSquares[number[squareIndexToCheck]].GetComponent<GridSquare>();
                if (comp.squareOccupied)
                {
                    shapeCanBePlacedOnBoard = false;
                }

            }
            if(shapeCanBePlacedOnBoard)
            {
                canBePlaced = true;
            }
        }
        return canBePlaced;
    }
    private List<int[]> GetAllSquaresCombination(int columns,int rows)
    {
        var squareList = new List<int[]>();
        var lastColumnIndex = 0;
        var lastRowIndex = 0;

        int safeIndex = 0;
        while(lastRowIndex+(rows-1) < 9)
        {
            var rowData = new List<int>();
            for(var row = lastRowIndex; row < lastRowIndex+rows; row++)
            {
                for(var column = lastColumnIndex; column < lastColumnIndex+columns; column++)
                {
                    rowData.Add(_lineIndicator.line_data[row, column]);
                }
            }
            squareList.Add(rowData.ToArray());
            lastColumnIndex++;
            if (lastColumnIndex + (columns - 1) >= 9)
            {
                lastRowIndex++;
                lastColumnIndex = 0;
            }
            safeIndex++;
            if(safeIndex > 100)
            {
                break;
            }
        }
        return squareList;
    }

}
