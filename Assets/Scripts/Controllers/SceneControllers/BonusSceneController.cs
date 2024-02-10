using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Views.Bonus;
using Models;
using Models.Values;
using Names;

namespace Controllers.SceneControllers
{
    public class BonusSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _spinBtn;
        [SerializeField] 
        private Button _homeBtn;
        
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private WheelView _wheelView;
        [SerializeField] 
        private ResultPanelView _resultPanelView;

        private BonusModel _bonusModel;
        private MenuModel _menuModel;

        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {

        }

        protected override void OnSceneDisable()
        {
        }

        protected override void Initialize()
        {
            _bonusModel = new BonusModel();
        }

        protected override void Subscribe()
        {
            _spinBtn.onClick.AddListener(RotateWheel);
            _homeBtn.onClick.AddListener(OpenMenuScene);
        }

        protected override void Unsubscribe()
        {
            _spinBtn.onClick.RemoveAllListeners();
            _homeBtn.onClick.RemoveAllListeners();
        }

        private void RotateWheel()
        {
            //base.SetClickClip();
            //base.TryPlaySound(_spinClip);

            int angle = _bonusModel.GetAngle;

            _wheelView.WheelEndRotationAction += SetWheelState;
            _wheelView.RotateWheel(angle);

            _spinBtn.interactable = false;
            _homeBtn.interactable = false;
        }

        private void SetWheelState()
        {
            _wheelView.WheelEndRotationAction -= SetWheelState;

            int prize = _bonusModel.GetPrize;
            
            Debug.Log(prize);
            
            //AudioClip clip = prize > 0 ? _winClip : _loseClip;

            //base.TryPlaySound(clip);

            if (prize > 0)
            {
                Wallet.AddMoney(prize);
                base.UpdateMoneyCountText();
            }

            StartCoroutine(DelayCheckSpinBtnState(prize>0, prize));
        }

        private void OpenResultPanel(bool isWin, int prize)
        {
            _resultPanelView.UpdateText(isWin, prize);
            _resultPanelView.gameObject.SetActive(true);
        }

        private void OpenMenuScene()
        {
            base.LoadScene(ScenesNames.Menu);
        }

        private IEnumerator DelayCheckSpinBtnState(bool isWin, int prize)
        {
            yield return new WaitForSecondsRealtime(2);

           OpenResultPanel(isWin, prize);
           
           yield return new WaitForSecondsRealtime(4);
           
           OpenMenuScene();
        }
    }
}