using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

using Controllers.ActionControllers.Game;
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

        public Vector2Int BoardSize => _boardSize;
        public bool CanDeleteBlock { get; set; }
        public bool CanSpawnPiece => _canSpawnPiece;

        private TetrominoData _nextTetrominoData;

        private RectInt Bounds
        {
            get
            {
                Vector2Int position = new Vector2Int(-_boardSize.x / 2, -_boardSize.y / 2);
                return new RectInt(position, _boardSize);
            }
        }

        private GameActionController _actionController;
        private bool _isFirstTime;
        private bool _canSpawnPiece;

        private void Awake()
        {
            for (int i = 0; i < _tetrominoes.Length; i++) 
            {
                _tetrominoes[i].Initialize();
            }
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !CanDeleteBlock) 
            {
                return;
            }

            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var tilePosition = _mainTileMap.WorldToCell(worldPoint);
            bool isDestroyed = false;

            if (_mainTileMap.GetTile(tilePosition) != null)
            {
                if (_activePiece.Cells.All(cell => cell + _activePiece.Position != tilePosition))
                {
                    isDestroyed = true;
                    _mainTileMap.SetTile(tilePosition, null);
                }
            }

            _actionController.BlockHasBeenDestroy.Invoke(isDestroyed);
                
            CanDeleteBlock = false;
        }

        public void StartGame(GameActionController actionController)
        {
            _actionController = actionController;
            _isFirstTime = true;
            _canSpawnPiece = true;
            
            SpawnPiece();
        }

        public void MoveItem(int direction)
        {
            Clear(_activePiece);
            
            Vector2Int directionVector = direction > 0 ? Vector2Int.right : Vector2Int.left;

            _activePiece.Move(directionVector);
            
            Set(_activePiece);
        }

        public void RotateItem()
        {
            Clear(_activePiece);
            
            _activePiece.Rotate(1);
            
            Set(_activePiece);
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
            if (!_canSpawnPiece)
            {
                return;
            }

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
            if (!_canSpawnPiece)
            {
                return;
            }

            if (_isFirstTime)
            {
                int random = Random.Range(0, _tetrominoes.Length);
                _nextTetrominoData = _tetrominoes[random];
            }

            _activePiece.Initialize(this, _spawnPosition, _nextTetrominoData);

            if (IsValidPosition(_activePiece, _spawnPosition))
            {
                Set(_activePiece);
            }
            else
            {
                GameOver();
            }

            int randomIndex = Random.Range(0, _tetrominoes.Length);

            _nextTetrominoData = _tetrominoes[randomIndex];
            
            _actionController.ChoseItem.Invoke(randomIndex, _isFirstTime);

            if (_isFirstTime)
            {
                _isFirstTime = false;
            }
        }
        
        public void ClearBoard()
        {
            Set(_activePiece);

            _mainTileMap.ClearAllTiles();

            if (!_canSpawnPiece)
            {
                return;
            }

            SpawnPiece();
        }

        public void ClearLines()
        {
            RectInt bounds = Bounds;
            int row = bounds.yMin;
            
            while (row < bounds.yMax)
            {
                if (IsLineFull(row)) {
                    LineClear(row);
                    _actionController.LineHasBeenDestroy.Invoke();
                } else {
                    row++;
                }
            }
        }
        
        private bool IsLineFull(int row)
        {
            RectInt bounds = Bounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                
                if (!_mainTileMap.HasTile(position)) {
                    return false;
                }
            }

            return true;
        }
        
        private void LineClear(int row)
        {
            RectInt bounds = Bounds;
            
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                _mainTileMap.SetTile(position, null);
            }
            
            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new Vector3Int(col, row + 1, 0);
                    TileBase above = _mainTileMap.GetTile(position);

                    position = new Vector3Int(col, row, 0);
                    _mainTileMap.SetTile(position, above);
                }

                row++;
            }
        }

        private void GameOver()
        {
            _canSpawnPiece = false;

            ClearBoard();
            
            _actionController.GameOver.Invoke();
        }
    }
}