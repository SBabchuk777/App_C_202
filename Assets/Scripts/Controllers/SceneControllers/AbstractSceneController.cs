using UnityEngine;

namespace Controllers.SceneControllers
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        private void OnEnable()
        {
            OnSceneEnable();
            Initialize();
            Subscribe();
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