using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonRewards;
        [SerializeField] private Button _buttonExit;

        public void Init(UnityAction startGame, UnityAction rewards, UnityAction exit)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonRewards.onClick.AddListener(rewards);
            _buttonExit.onClick.AddListener(exit);
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonRewards.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }

        public void Show()
        {

        }

        public void Hide()
        {

        }
    }
}