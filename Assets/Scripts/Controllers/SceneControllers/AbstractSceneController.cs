using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Models;

namespace Controllers.SceneControllers
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
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
            
            Initialize();
            Subscribe();
            OnSceneEnable();
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

        protected void LoadScene(string sceneName)
        {
            StartCoroutine(DelayLoadScene(sceneName));
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