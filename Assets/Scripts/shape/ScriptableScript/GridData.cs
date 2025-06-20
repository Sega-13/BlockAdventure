using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBlockAdv.SquareShape
{
    [CreateAssetMenu(fileName = "GridData", menuName = "ScriptableObjects/GridData")]
    public class GridData : ScriptableObject
    {
        [Serializable]
        public class IntList
        {
            public List<int> row = new List<int>();
        }

        public List<IntList> line_data = new List<IntList>();
        public List<IntList> square_data = new List<IntList>();
        public List<int> columnIndexes = new List<int>();
    }
}

