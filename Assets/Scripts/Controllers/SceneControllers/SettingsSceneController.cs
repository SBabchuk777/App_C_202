using Enums;
using UnityEngine;
using UnityEngine.UI;

using Views.Settings;
using Names;
using Sounds;

namespace Controllers.SceneControllers
{
    public class SettingsSceneController : AbstractSceneController
    {
        [Space(5)][Header("Buttons")]
        [SerializeField] 
        private SwitcherBtn _soundSwitcherView;
        [SerializeField] 
        private SwitcherBtn _musicSwitcherView;
        [SerializeField] 
        private Button _ppBtn;
        [SerializeField] 
        private Button _termsBtn;
        [SerializeField] 
        private Button _backBtn;
        
        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {
            UpdateSoundBtnSprite();
            UpdateMusicBtnSprite();
            base.PlayMusic(GetAudioClip(AudioNames.MenuClip.ToString()));
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Subscribe()
        {
            _ppBtn.onClick.AddListener(delegate { base.SetClickClip(); });
            _termsBtn.onClick.AddListener(delegate { base.SetClickClip(); });
            _backBtn.onClick.AddListener(delegate { LoadScene(ScenesNames.Menu); });

            _soundSwitcherView.PressBtnAction += ChangeSoundState;
            _musicSwitcherView.PressBtnAction += ChangeMusicState;

        }

        protected override void Initialize()
        {
            
        }

        protected override void Unsubscribe()
        {
            _ppBtn.onClick.RemoveAllListeners();
            _termsBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
            
            _soundSwitcherView.PressBtnAction += ChangeSoundState;
            _musicSwitcherView.PressBtnAction += ChangeMusicState;
        }

        private void UpdateSoundBtnSprite()
        {
            _soundSwitcherView.SetSprite(SoundsStates.CanPlaySound ? 0 : 1);
        }

        private void UpdateMusicBtnSprite()
        {
            _musicSwitcherView.SetSprite(SoundsStates.CanPlayMusic ? 0: 1);
        }

        private void ChangeSoundState()
        {
            SoundsStates.ChangeSoundsState();
            
            base.SetClickClip();
            
            UpdateSoundBtnSprite();
        }

        private void ChangeMusicState()
        {
            base.SetClickClip();

            SoundsStates.ChangeMusicState();
            
            base.PlayMusic(GetAudioClip(AudioNames.MenuClip.ToString()));
            
            UpdateMusicBtnSprite();
        }
    }
}