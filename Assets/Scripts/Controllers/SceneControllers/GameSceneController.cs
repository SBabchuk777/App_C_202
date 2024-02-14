using UnityEngine;
using UnityEngine.UI;

using Controllers.Game;
using Controllers.ActionControllers.Game;

using Models.Game;
using Views.Game;

using Enums;
using Names;

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
            
            base.PlayMusic(GetAudioClip(AudioNames.GameClip.ToString()));
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
            _leftMoveBtn.onClick.AddListener(SetLeftMoveItem);
            _rightMoveBtn.onClick.AddListener(SetRightMoveItem);
            _rotateBtn.onClick.AddListener(SetRotationItem);
            _pauseBtn.onClick.AddListener(OpenPausePanel);
            
            _clearBoardView.PressBtnAction += ClearBoard;
            _deleteBlockView.PressBtnAction += SetCanDeleteBlock;
            _actionController.ChoseItem += SetPreviewItem;
            _actionController.LineHasBeenDestroy += delegate { AddScore(0); };
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
            _actionController.LineHasBeenDestroy -= delegate { AddScore(0); };
            _actionController.GameOver -= OpenResultPanel;
        }

        private void ClearBoard()
        {
            base.SetClickClip();
            base.PlaySound(GetAudioClip(AudioNames.DeleteLine.ToString()));
            
            _boardController.ClearBoard();
            
            AddScore(3);

            base.ClearBoardBoosterCount--;
            
            SetBoosterCount();
            CheckActiveBoosterBtns();
        }

        private void SetCanDeleteBlock()
        {
            SetClickClip();

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

            base.PlaySound(GetAudioClip(AudioNames.DeleteLine.ToString()));
            
            base.DestroyBlockBoosterCount--;
            SetBoosterCount();
            CheckActiveBoosterBtns();
            AddScore(1);
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

        private void SetPreviewItem(int index, bool isFirstTime)
        {
            _previewItemView.SetSprite(index);

            if (isFirstTime)
            {
                return;
            }

            AddScore(2);
        }

        private void AddScore(int index)
        {
            if (index == 0)
            {
                base.PlaySound(GetAudioClip(AudioNames.DeleteLine.ToString()));
            }

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
            base.SetClickClip();
            
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
                    base.SetClickClip();
                    
                    _pausePanelView.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case 1:
                    base.LoadScene(ScenesNames.Menu);
                    break;
            }
        }

        private void SetLeftMoveItem()
        {
            base.SetClickClip();
            _boardController.MoveItem(-1);
        }
        
        private void SetRightMoveItem()
        {
            base.SetClickClip();
            _boardController.MoveItem(1);
        }

        private void SetRotationItem()
        {
            base.SetClickClip();

            _boardController.RotateItem();
        }
    }
}