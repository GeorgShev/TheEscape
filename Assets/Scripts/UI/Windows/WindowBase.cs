using Data;
using Services.PersistentProgressService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;

        protected IPersistentProgressService PersistantProgressService;
        protected PlayerProgress PlayerProgress => PersistantProgressService.Progress;

        public void Construct(IPersistentProgressService persistentProgressService)
        {
            PersistantProgressService = persistentProgressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Clenup();
        }

        protected virtual void OnAwake()
        {
            if (CloseButton)
            {
                CloseButton.onClick.AddListener(() => Destroy(gameObject));
            }
        }

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Clenup(){}
    }
}
