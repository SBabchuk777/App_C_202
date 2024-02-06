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
        public Tetromino _tetromino;
        public Tile _tile;
        public Vector2[] _cells { get; private set; }

        public void Initialize()
        {
            this._cells = GameData.Cells[this._tetromino];
        }
    }
}
