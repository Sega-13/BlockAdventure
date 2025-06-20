using System;
using UnityEngine;
using GameBlockAdv.SquareShape;

namespace GameBlockAdv
{
    public class GameEvents : MonoBehaviour
    {
        public static Action<bool> GameOver;
        public static Action<int> AddScores;
        public static Action CheckIfShapeCanBePlaced;
        public static Action MoveShapeToStartPosition;
        public static Action RequestNewShapes;
        public static Action CheckIfPlayerLost;
        public static Action SetShapeInactive;
        public static Action<int, int> UpdateBestScoreBar;
        public static Action<SquareColor> UpdateSquareColor;
        public static Action ShowCongratulations;
        public static Action<SquareColor> ShowBonusScreen;
    }

}

