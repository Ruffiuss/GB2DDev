using Features.LocalizationFeature;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Core
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonRewards;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonLocalize;

        private List<Button> _buttons = new List<Button>();

        public void Init(Locale locale, UnityAction startGame, UnityAction rewards, UnityAction exit, UnityAction localize)
        {
            _buttons.Add(_buttonStart); _buttons.Add(_buttonRewards); _buttons.Add(_buttonExit); _buttons.Add(_buttonLocalize);

            _buttonStart.onClick.AddListener(startGame);
            _buttonRewards.onClick.AddListener(rewards);
            _buttonExit.onClick.AddListener(exit);
            _buttonLocalize.onClick.AddListener(localize);
        }

        protected void OnDestroy()
        {
            foreach (var button in _buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        public void Show(){}

        public void Hide(){}
    }
}