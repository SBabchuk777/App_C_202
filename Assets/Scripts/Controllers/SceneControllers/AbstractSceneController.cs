using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Models;
using Models.Values;
using SO;
using Sounds;

namespace Controllers.SceneControllers
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        [SerializeField] 
        private Text _moneyCountText;
        [SerializeField] 
        private SoundsController _soundsController;
        [SerializeField]
        private SceneSounds _sceneSounds;
        
        private MusicController _musicController;

        protected int ClearBoardBoosterCount
        {
            get => _model.ClearBoardBoosterCount;
            set => _model.ClearBoardBoosterCount = value;
        }

        protected int DestroyBlockBoosterCount
        {
            get => _model.DestroyBlockBoosterCount;
            set => _model.DestroyBlockBoosterCount = value;
        }
        
        private MainModel _model;

        private void OnEnable()
        {
            _model = new MainModel();
            _musicController = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>();
            
            _sceneSounds.SetAudioClip();
            
            Initialize();
            Subscribe();
            OnSceneEnable();
            UpdateMoneyCountText();
        }

        private void Start()
        {
            OnSceneStart();
        }

        private void OnDisable()
        {
            OnSceneDisable();
            Unsubscribe();
        }

        protected abstract void OnSceneEnable();
        protected abstract void OnSceneStart();
        protected abstract void OnSceneDisable();
        protected abstract void Initialize();
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected void UpdateMoneyCountText()
        {
            if (_moneyCountText == null)
            {
                return;
            }

            _moneyCountText.text = Wallet.MoneyCount.ToString();
        }

        protected void SetClickClip()
        {
            PlaySound(GetAudioClip(AudioNames.ClickClip.ToString()));
        }

        protected void LoadScene(string sceneName)
        {
            SetClickClip();
            StartCoroutine(DelayLoadScene(sceneName));
        }

        protected AudioClip GetAudioClip(string clipName)
        {
            return _sceneSounds.GetAudioClipByName(clipName);
        }

        protected void PlaySound(AudioClip clip)
        {
            _soundsController.TryPlaySound(clip);
        }

        protected void PlayMusic(AudioClip clip)
        {
            _musicController.TryPlayMusic(clip);
        }

        protected void StopMusic()
        {
            _musicController.StopMusic();
        }

        private IEnumerator DelayLoadScene(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}