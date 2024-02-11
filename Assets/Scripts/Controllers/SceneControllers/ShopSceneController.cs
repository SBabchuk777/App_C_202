using UnityEngine;
using UnityEngine.UI;

using Views.Shop;
using Models;
using Models.Values;

using Names;
using Enums;

namespace Controllers.SceneControllers
{
    public class ShopSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _closeBtn;
        [SerializeField] 
        private BuyBtn _firstBuyBtn;
        [SerializeField] 
        private BuyBtn _secondBuyBtn;

        [Space(5)] [Header("Views")] 
        [SerializeField]
        private ShopPanelView _panelView;

        private ShopModel _model;
        private int _currentBoosterIndex;
        
        protected override void OnSceneEnable()
        {
            CheckActiveBuyBtns();
        }

        protected override void OnSceneStart()
        {
            base.PlayMusic(GetAudioClip(AudioNames.MenuClip.ToString()));
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new ShopModel();
        }

        protected override void Subscribe()
        {
            _closeBtn.onClick.AddListener(delegate { base.LoadScene(ScenesNames.Menu); });
            
            _firstBuyBtn.PressBtnAction += delegate { BuyBooster(0); };
            _secondBuyBtn.PressBtnAction += delegate { BuyBooster(1); };
        }

        protected override void Unsubscribe()
        {
            _closeBtn.onClick.RemoveAllListeners();
            
            _firstBuyBtn.PressBtnAction -= delegate { BuyBooster(0); };
            _secondBuyBtn.PressBtnAction -= delegate { BuyBooster(1); };
        }

        private void BuyBooster(int index)
        {
            _currentBoosterIndex = index;
            
            base.SetClickClip();
            
            OpenConfirmPanel();
        }

        private void CheckActiveBuyBtns()
        {
            _firstBuyBtn.SetBtnActive(_model.IsActiveBtn(Wallet.MoneyCount, _model.FirstBoosterCost));
            _secondBuyBtn.SetBtnActive(_model.IsActiveBtn(Wallet.MoneyCount, _model.SecondBoosterCost));
        }

        private void OpenConfirmPanel()
        {
            _panelView.PressBtnAction += CheckAnswerConfirmPanel;
            _panelView.gameObject.SetActive(true);
        }

        private void CheckAnswerConfirmPanel(int answer)
        {
            base.SetClickClip();
            
            _panelView.PressBtnAction -= CheckAnswerConfirmPanel;
            _panelView.gameObject.SetActive(false);

            if (answer == 0)
            {
                return;
            }

            if (_currentBoosterIndex == 0)
            {
                base.DestroyBlockBoosterCount++;
                Wallet.TryPurchase(_model.FirstBoosterCost);
            }
            else
            {
                base.ClearBoardBoosterCount++;
                Wallet.TryPurchase(_model.SecondBoosterCost);
            }
            
            base.PlaySound(GetAudioClip(AudioNames.BuyClip.ToString()));
            CheckActiveBuyBtns();
            base.UpdateMoneyCountText();
        }
    }
}