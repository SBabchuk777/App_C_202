using UnityEngine;
using UnityEngine.Tilemaps;
using Enums.Game;

namespace Controllers.Game
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField]
        private TetrominoData[] _tetrominoes;
        [SerializeField] 
        private Piece _activePiece;
        [SerializeField] 
        private Tilemap _mainTileMap;
        [SerializeField] 
        private Vector3Int _spawnPosition;
        [SerializeField] 
        private Vector2Int _boardSize;

        public RectInt Bounds
        {
            get
            {
                Vector2Int position = new Vector2Int(-_boardSize.x / 2, -_boardSize.y / 2);
                return new RectInt(position, _boardSize);
            }
        }

        private void Awake()
        {
            for (int i = 0; i < _tetrominoes.Length; i++) 
            {
                _tetrominoes[i].Initialize();
            }
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            SpawnPiece();
        }

        public bool IsValidPosition(Piece piece, Vector3Int position)
        {
            RectInt bounds = Bounds;
            
            foreach (var pieceCell in piece.Cells)
            {
                Vector3Int tilePos = pieceCell + position;

                if (!bounds.Contains((Vector2Int)tilePos))
                {
                    return false;
                }

                if (_mainTileMap.HasTile(tilePos))
                {
                    return false;
                }
            }

            return true;
        }
        
        public void Set(Piece piece)
        {
            foreach (var pieceCell in piece.Cells)
            {
                Vector3Int tilePos = pieceCell + piece.Position;
                _mainTileMap.SetTile(tilePos, piece.Data._tile);
            }
        }
        
        public void Clear(Piece piece)
        {
            foreach (var pieceCell in piece.Cells)
            {
                Vector3Int tilePos = pieceCell + piece.Position;
                _mainTileMap.SetTile(tilePos, null);
            }
        }
        
        public void SpawnPiece()
        {
            int randomIndex = Random.Range(0, _tetrominoes.Length);
            TetrominoData data = _tetrominoes[randomIndex];

            _activePiece.Initialize(this, _spawnPosition, data);
            Set(_activePiece);
        }
    }
}