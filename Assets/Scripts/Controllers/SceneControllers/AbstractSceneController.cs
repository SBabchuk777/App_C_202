using UnityEngine;
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
    }
}