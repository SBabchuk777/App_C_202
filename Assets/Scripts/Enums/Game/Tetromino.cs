using Models.Game;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enums.Game
{
    public enum Tetromino
    {
        I,
        O,
        T,
        J,
        L,
        S,
        Z
    }

    [System.Serializable]
    public struct TetrominoData
    {
        public Tile _tile;
        public Tetromino _tetromino;
        public Vector2Int[] Cells { get; private set; }
        public Vector2Int[,] WallKicks { get; private set; }

        public void Initialize()
        {
            Cells = GameData.Cells[_tetromino];
            WallKicks = GameData.WallKicks[_tetromino];
        }
    }
}
