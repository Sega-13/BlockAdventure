using UnityEditor;
using UnityEngine;

namespace GameBlockAdv.SquareShape
{
    [CustomEditor(typeof(GridData))]
    public class GridDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();

            GridData data = (GridData)target;

            if (GUILayout.Button("Initialize Empty 9x9 Grids"))
            {
                InitializeGrids(data);
                EditorUtility.SetDirty(data);
            }

            if (GUILayout.Button("Fill Grids with Default Sudoku Values"))
            {
                FillDefaultGrids(data);
                EditorUtility.SetDirty(data);
            }
        }

        private void InitializeGrids(GridData data)
        {
            data.line_data.Clear();
            data.square_data.Clear();

            for (int i = 0; i < 9; i++)
            {
                var lineRow = new GridData.IntList();
                var squareRow = new GridData.IntList();

                for (int j = 0; j < 9; j++)
                {
                    lineRow.row.Add(0);
                    squareRow.row.Add(0);
                }

                data.line_data.Add(lineRow);
                data.square_data.Add(squareRow);
            }

            data.columnIndexes = new System.Collections.Generic.List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        private void FillDefaultGrids(GridData data)
        {
            InitializeGrids(data); // Start fresh

            // Fill line_data
            int index = 0;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    data.line_data[row].row[col] = index++;
                }
            }

            // Fill square_data
            int[,] squareDataSource = new int[9, 9]
            {
            { 0, 1, 2, 9, 10, 11, 18, 19, 20},
            { 3, 4, 5, 12, 13, 14, 21, 22, 23},
            { 6, 7, 8, 15, 16, 17, 24, 25, 26},
            {27,28,29,36,37,38,45,46,47},
            {30,31,32,39,40,41,48,49,50},
            {33,34,35,42,43,44,51,52,53},
            {54,55,56,63,64,65,72,73,74},
            {57,58,59,66,67,68,75,76,77},
            {60,61,62,69,70,71,78,79,80}
            };

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    data.square_data[row].row[col] = squareDataSource[row, col];
                }
            }
        }
    }
}

