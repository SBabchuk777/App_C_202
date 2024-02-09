using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers.Game
{
    public class GhostController : MonoBehaviour
    {
        [SerializeField] 
        private Tile _tile;
        [SerializeField] 
        private BoardController _board;
        [SerializeField] 
        private Piece _piece;
        [SerializeField] 
        private Tilemap _ghostTileMap;
        
        public Vector3Int[] Cells { get; private set; }
        public Vector3Int Position { get; private set; }

        private void Awake()
        {
            Cells = new Vector3Int[4];
        }

        private void LateUpdate()
        {
            Clear();
            Copy();
            Drop();
            Set();
        }

        private void Clear()
        {
            foreach (var pieceCell in Cells)
            {
                Vector3Int tilePos = pieceCell + Position;
                _ghostTileMap.SetTile(tilePos, null);
            }
        }

        private void Copy()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = _piece.Cells[i];
            }
        }

        private void Drop()
        {
            Vector3Int position = _piece.Position;

            int current = position.y;
            int bottom = -_board.BoardSize.y / 2 - 1;
            
            _board.Clear(_piece);

            for (int row = current; row >= bottom; row--)
            {
                position.y = row;

                if (_board.IsValidPosition(_piece, position))
                {
                    Position = position;
                }
                else
                {
                    break;
                }
            }
            
            _board.Set(_piece);
        }

        private void Set()
        {
            foreach (var pieceCell in Cells)
            {
                Vector3Int tilePos = pieceCell + Position;
                _ghostTileMap.SetTile(tilePos, _tile);
            }
        }
    }
}