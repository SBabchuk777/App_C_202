using UnityEngine;
using UnityEngine.UI;

using Controllers.Game;
using Controllers.ActionControllers.Game;
using Views.Game;

namespace Controllers.SceneControllers
{
    public class GameSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Controllers")] 
        [SerializeField]
        private BoardController _boardController;
        
        [Space(5)][Header("Buttons")]
        [SerializeField] 
        private Button _leftMoveBtn;
        [SerializeField] 
        private Button _rightMoveBtn;
        [SerializeField] 
        private Button _rotateBtn;

        [Space(5)][Header("Views")]
        [SerializeField] 
        private BoosterView _clearBoardView;
        [SerializeField] 
        private BoosterView _deleteBlockView;

        private GameActionController _actionController;
        
        protected override void OnSceneEnable()
        {
        }

        protected override void OnSceneStart()
        {
            _boardController.StartGame(_actionController);
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _actionController = new GameActionController();
        }

        protected override void Subscribe()
        {
            _leftMoveBtn.onClick.AddListener(delegate { _boardController.MoveItem(-1); });
            _rightMoveBtn.onClick.AddListener(delegate { _boardController.MoveItem(1); });
            _rotateBtn.onClick.AddListener(_boardController.RotateItem);
            
            _clearBoardView.PressBtnAction += ClearBoard;
            _deleteBlockView.PressBtnAction += SetCanDeleteBlock;
        }

        protected override void Unsubscribe()
        {
            _leftMoveBtn.onClick.RemoveAllListeners();
            _rightMoveBtn.onClick.RemoveAllListeners();
            _rotateBtn.onClick.RemoveAllListeners();
            
            _clearBoardView.PressBtnAction -= ClearBoard;
            _deleteBlockView.PressBtnAction -= SetCanDeleteBlock;
        }

        private void ClearBoard()
        {
            _boardController.ClearBoard();
        }

        private void SetCanDeleteBlock()
        {
            _boardController.CanDeleteBlock = true;

            _actionController.BlockHasBeenDestroy += UpdateCountDeleteBlockBooster;
            
            _deleteBlockView.SetInteractableBtn(false);
        }

        private void UpdateCountDeleteBlockBooster(bool blockHasBeenDeleted)
        {
            _actionController.BlockHasBeenDestroy -= UpdateCountDeleteBlockBooster;
            
            _deleteBlockView.SetInteractableBtn(true);
            
            Debug.Log("Block Has Been Deleted" + blockHasBeenDeleted);
        }
    }
}