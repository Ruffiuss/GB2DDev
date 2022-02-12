using System;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Data
{
    public class AbilityView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Button _button;
        private Image _imageComponent;
        private Sprite _abilitySprite;
        private IItem _item;

        #endregion

        #region Properties

        public event Action<IItem> OnClick;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _button.onClick.AddListener(Click);
            _imageComponent = _button.GetComponent<Image>();
        }

        private void OnDestroy()
        {
            OnClick = null;
            _button.onClick.RemoveAllListeners();
        }

        #endregion

        #region Methods

        public void Init(IItem item)
        {
            _item = item;
            _abilitySprite = item.Sprite;
            _imageComponent.sprite = _abilitySprite;
        }

        private void Click()
        {
            OnClick?.Invoke(_item);
        }

        #endregion
    }
}
