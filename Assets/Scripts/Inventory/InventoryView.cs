using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{
    #region Fields

    private CanvasGroup _canvasGroup;
    private ToggleGroup _toggleGroup;

    private Dictionary<IItem, IInventoryItemView> _currentItems;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _toggleGroup = GetComponent<ToggleGroup>();
        _currentItems = new Dictionary<IItem, IInventoryItemView>();
        Hide();
    }

    #endregion

    #region Methods

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach(var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }

    public void LoadShedUI(UnityAction<IItem, bool> toggleHandler, IReadOnlyDictionary<IItem, bool> items, ResourcePath itemPrefabPath)
    {
        var prefab = ResourceLoader.LoadPrefab(itemPrefabPath);
        foreach (var item in items)
        {
            var itemView = GameObject.Instantiate(prefab, gameObject.transform).GetComponent<IInventoryItemView>();
            itemView.Init(toggleHandler, item.Key, item.Value);
            _currentItems.Add(item.Key, itemView);
        }
    }

    public void OnDispose()
    {
        foreach (var item in _currentItems)
        {
            item.Value.OnDispose();
        }
        _currentItems.Clear();
        Destroy(gameObject);
    }

    #endregion
}
