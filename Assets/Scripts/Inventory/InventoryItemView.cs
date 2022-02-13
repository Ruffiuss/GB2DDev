using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class InventoryItemView : MonoBehaviour, IInventoryItemView
{
    #region Fields

    private IItem _item;
    private Toggle _toggle;
    private UnityAction<IItem, bool> _parentToggleHandler;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.enabled = false;
    }

    #endregion

    #region Methods

    public void Init(UnityAction<IItem, bool> toggleHandler, IItem item, bool isOn)
    {
        _item = item;
        _toggle.isOn = isOn;
        _toggle.GetComponentInChildren<Text>().text = _item.Info.Title;
        _parentToggleHandler = toggleHandler;
        _toggle.onValueChanged.AddListener(AddListenerProxy);
        _toggle.enabled = true;
    }

    private void AddListenerProxy(bool isOn)
    {
        _parentToggleHandler.Invoke(_item, isOn);
    }

    public void OnDispose()
    {
        _toggle.onValueChanged.RemoveAllListeners();
        _parentToggleHandler = null;
        Destroy(gameObject);
    }

    #endregion    
}
