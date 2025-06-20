using UnityEngine;

namespace GameBlockAdv.SquareShape
{
    public class LineIndicator : MonoBehaviour
    {
        [SerializeField] private GridData gridData;

        public int GetLineData(int row, int col) => gridData.line_data[row].row[col];
        public int GetSquareData(int row, int col) => gridData.square_data[row].row[col];

        [HideInInspector]
        public int[] columnIndexs = new int[9]
        {
         0,1,2,3,4,5,6,7,8
        };
        private (int, int) GetSquarePosition(int squareIndex)
        {
            int pos_row = -1;
            int pos_col = -1;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (GetLineData(row, col) == squareIndex)
                    {
                        pos_row = row;
                        pos_col = col;
                    }
                }
            }
            return (pos_row, pos_col);

        }
        public int[] GetVerticalLine(int squareIndex)
        {
            int[] line = new int[9];
            var square_position_column = GetSquarePosition(squareIndex).Item2;
            for (int index = 0; index < 9; index++)
            {
                line[index] = GetLineData(index, square_position_column);
            }
            return line;
        }
        public int GetGridSquareIndex(int square)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (GetLineData(row, col) == square)
                    {
                        return row;
                    }
                }
            }
            return -1;
        }

    }
}

