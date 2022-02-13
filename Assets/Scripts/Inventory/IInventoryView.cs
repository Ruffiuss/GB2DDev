using System.Collections.Generic;

public interface IInventoryView
{
    void LoadShedUI(UnityEngine.Events.UnityAction<IItem, bool> toggleHandler, IReadOnlyDictionary<IItem, bool> items, ResourcePath itemPrefabPath);
    void Display(IReadOnlyList<IItem> items);
    void Show();
    void Hide();
    void OnDispose();
}
