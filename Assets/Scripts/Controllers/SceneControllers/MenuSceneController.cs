using UnityEngine;
using UnityEngine.UI;

using Models;
using Names;

namespace Controllers.SceneControllers
{
    public class MenuSceneController : AbstractSceneController
    {
        [Space(5)][Header("Buttons")]
        [SerializeField] 
        private Button _playBtn;
        [SerializeField] 
        private Button _shopBtn;
        [SerializeField]
        private Button _bonusBtn;
        [SerializeField] 
        private Button _settingsBtn;

        private MenuModel _model;
            
        protected override void OnSceneEnable()
        {
            CheckActiveBonusBtn();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new MenuModel();
        }

        protected override void Subscribe()
        {
            _playBtn.onClick.AddListener(delegate { base.LoadScene(ScenesNames.Game); });
            _shopBtn.onClick.AddListener(delegate { base.LoadScene(ScenesNames.Shop); });
            _bonusBtn.onClick.AddListener(delegate { base.LoadScene(ScenesNames.Bonus); });
            _settingsBtn.onClick.AddListener(delegate { base.LoadScene(ScenesNames.Settings); });
        }

        protected override void Unsubscribe()
        {
           _playBtn.onClick.RemoveAllListeners();
           _shopBtn.onClick.RemoveAllListeners();
           _bonusBtn.onClick.RemoveAllListeners();
           _settingsBtn.onClick.RemoveAllListeners();
        }

        private void CheckActiveBonusBtn()
        {
            _bonusBtn.gameObject.SetActive(_model.CanShowBonusBtn);
        }
    }
}