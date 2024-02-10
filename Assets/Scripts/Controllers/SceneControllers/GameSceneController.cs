using UnityEngine;
using UnityEngine.UI;

using Controllers.Game;
using Controllers.ActionControllers.Game;

using Models.Game;
using Views.Game;
using Names;
using Unity.VisualScripting;

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
        [SerializeField] 
        private Button _pauseBtn;

        [Space(5)][Header("Views")]
        [SerializeField] 
        private BoosterView _clearBoardView;
        [SerializeField] 
        private BoosterView _deleteBlockView;
        [SerializeField] 
        private PreviewItemView _previewItemView;
        [SerializeField] 
        private ScoreView _scoreView;
        [SerializeField] 
        private GamePanelView _resultPanelView;
        [SerializeField] 
        private GamePanelView _pausePanelView;

        private GameModel _model;
        private GameActionController _actionController;

        protected override void OnSceneEnable()
        {
            SetBoosterCount();
            CheckActiveBoosterBtns();
            UpdateScore();
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
            _model = new GameModel();
        }

        protected override void Subscribe()
        {
            _leftMoveBtn.onClick.AddListener(delegate { _boardController.MoveItem(-1); });
            _rightMoveBtn.onClick.AddListener(delegate { _boardController.MoveItem(1); });
            _rotateBtn.onClick.AddListener(_boardController.RotateItem);
            _pauseBtn.onClick.AddListener(OpenPausePanel);
            
            _clearBoardView.PressBtnAction += ClearBoard;
            _deleteBlockView.PressBtnAction += SetCanDeleteBlock;
            _actionController.ChoseItem += SetPreviewItem;
            _actionController.LineHasBeenDestroy += delegate { AddScore(1); };
            _actionController.GameOver += OpenResultPanel;
        }

        protected override void Unsubscribe()
        {
            _leftMoveBtn.onClick.RemoveAllListeners();
            _rightMoveBtn.onClick.RemoveAllListeners();
            _rotateBtn.onClick.RemoveAllListeners();
            _pauseBtn.onClick.RemoveAllListeners();
            
            _clearBoardView.PressBtnAction -= ClearBoard;
            _deleteBlockView.PressBtnAction -= SetCanDeleteBlock;
            _actionController.ChoseItem -= SetPreviewItem;
            _actionController.LineHasBeenDestroy -= delegate { AddScore(1); };
            _actionController.GameOver -= OpenResultPanel;
        }

        private void ClearBoard()
        {
            _boardController.ClearBoard();

            base.ClearBoardBoosterCount--;
            
            SetBoosterCount();
            CheckActiveBoosterBtns();
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

            if (!blockHasBeenDeleted)
            {
                return;
            }

            base.DestroyBlockBoosterCount--;
            SetBoosterCount();
            CheckActiveBoosterBtns();
            AddScore(5);
        }

        private void CheckActiveBoosterBtns()
        {
            _clearBoardView.SetInteractableBtn(_model.IsActiveBoosterBtn(base.ClearBoardBoosterCount));
            _deleteBlockView.SetInteractableBtn(_model.IsActiveBoosterBtn(base.DestroyBlockBoosterCount));
        }

        private void SetBoosterCount()
        {
            _clearBoardView.SetCountText(base.ClearBoardBoosterCount);
            _deleteBlockView.SetCountText(base.DestroyBlockBoosterCount);
        }

        private void SetPreviewItem(int index, int countBlocks)
        {
            _previewItemView.SetSprite(index);
            
            AddScore(countBlocks);
        }

        private void AddScore(int index)
        {
            _model.UpdateScore(index);
            
            UpdateScore();
        }

        private void UpdateScore()
        {
            _scoreView.UpdateText(_model.Score);
        }

        private void OpenResultPanel()
        {
            _resultPanelView.SetText(_model.Score);
            _resultPanelView.PressBtnAction += CheckAnswerResultPanel;
            _resultPanelView.gameObject.SetActive(true);
        }

        private void OpenPausePanel()
        {
            Time.timeScale = 0;

            _pausePanelView.PressBtnAction += CheckAnswerPausePanel;
            _pausePanelView.gameObject.SetActive(true);
        }

        private void CheckAnswerResultPanel(int answer)
        {
            _resultPanelView.PressBtnAction -= CheckAnswerResultPanel;

            switch (answer)
            {
                case 0:
                    base.LoadScene(ScenesNames.Menu);
                    break;
                case 1:
                    base.LoadScene(ScenesNames.Game);
                    break;
            }
        }

        private void CheckAnswerPausePanel(int answer)
        {
            _pausePanelView.PressBtnAction -= CheckAnswerPausePanel;

            switch (answer)
            {
                case 0:
                    _pausePanelView.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case 1:
                    base.LoadScene(ScenesNames.Menu);
                    break;
            }
        }
    }
}