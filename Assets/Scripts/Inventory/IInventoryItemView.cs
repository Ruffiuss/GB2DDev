public interface IInventoryItemView
{
    void Init(UnityEngine.Events.UnityAction<IItem, bool> toggleHandler, IItem item, bool isOn);
    void OnDispose();
}
