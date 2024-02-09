using System;
using UnityEngine;
using Enums.Game;
using Models.Game;

namespace Controllers.Game
{
    public class Piece : MonoBehaviour
    {
        public BoardController Board { get; private set; }
        public TetrominoData Data { get; private set; }
        public Vector3Int[] Cells { get; private set; }
        public Vector3Int Position { get; private set; }
        public int RotationIndex { get; private set; }

        public float _stepDelay = 1f;
        public float _lockDelay = 0.5f;

        private float _stepTime;
        private float _lockTime;

        private void Update()
        {
            Board.Clear(this);

            _lockTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rotate(-1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Rotate(1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2Int.left);
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2Int.right);
            }
            
            if(Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2Int.down);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HardDrop();
            }

            if (Time.time >= _stepTime)
            {
                Step();
            }

            Board.Set(this);
        }

        public void Initialize(BoardController board, Vector3Int position, TetrominoData data)
        {
            Board = board;
            Data = data;
            Position = position;
            RotationIndex = 0;

            _stepTime = Time.time + _stepDelay;
            _lockTime = 0;

            Cells ??= new Vector3Int[4];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = (Vector3Int)data.Cells[i];
            }
        }

        private void Step()
        {
            _stepTime = Time.time + _stepDelay;

            Move(Vector2Int.down);

            if (_lockTime >= _lockDelay)
            {
                Lock();
            }
        }

        private void Lock()
        {
            Board.Set(this);
            Board.ClearLines();
            Board.SpawnPiece();
        }

        private void HardDrop()
        {
            while (Move(Vector2Int.down))
            {
                continue;
            }
            
            Lock();
        }

        private bool Move(Vector2Int translation)
        {
            Vector3Int newPosition = Position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = Board.IsValidPosition(this,newPosition);

            if (valid)
            {
                Position = newPosition;
                _lockTime = 0;
            }

            return valid;
        }

        private void Rotate(int direction)
        {
            int originalRotation = RotationIndex;
            RotationIndex += Wrap(RotationIndex + direction, 0, 4);
            
            ApplyRotationMatrix(direction);

            if (!TestWallKicks(RotationIndex, direction))
            {
                RotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        private void ApplyRotationMatrix(int direction)
        {
            float[] matrix = GameData.RotationMatrix;
            
            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3 cell = Cells[i];

                int x, y;

                switch (Data._tetromino)
                {
                    case Tetromino.I:
                    case Tetromino.O:
                        // "I" and "O" are rotated from an offset center point
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;

                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                }

                Cells[i] = new Vector3Int(x, y, 0);
            }
        }

        private bool TestWallKicks(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

            for (int i = 0; i < Data.WallKicks.GetLength(1); i++)
            {
                Vector2Int translation = Data.WallKicks[wallKickIndex, i];

                if (Move(translation))
                {
                    return true;
                }
            }

            return true;
        }

        private int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;

            if (rotationDirection < 0)
            {
                wallKickIndex--;
            }

            return Wrap(wallKickIndex, 0, Data.WallKicks.GetLength(0));
        }

        private int Wrap(int input, int min, int max)
        {
            if (input < min)
            {
                return max - (min - input) % (max - min);
            }
            else
            {
                return min + (input - min) % (max - min);
            }
        }
    }
}