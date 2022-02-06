using Tools;
using UnityEngine;
using UnityEngine.UI;


namespace Shop
{
    [RequireComponent(typeof(Text))]
    public class GoldBalanceView : MonoBehaviour
    {
        #region Fields

        private Text _textContainer;
        private SubscriptionProperty<ushort> _goldCountProperty;

        private string _header;
        private ushort _goldCount;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _textContainer = gameObject.GetComponent<Text>();
        }

        private void OnDestroy()
        {
            _goldCountProperty.UnSubscriptionOnChange(OnGoldCountChange);
        }

        #endregion

        #region Methods

        public void Init(string header, SubscriptionProperty<ushort> goldCount)
        {
            _header = header;
            _goldCountProperty = goldCount;
            _goldCountProperty.SubscribeOnChange(OnGoldCountChange);
            _textContainer.text = _header + goldCount.Value;
        }

        private void OnGoldCountChange(ushort count)
        {
            _textContainer.text = _header + count;
        }

        #endregion
    }
}
