public interface IInventoryController
{
    void InitShedUI(UnityEngine.Transform placeForUI, ResourcePath layoutPrefabPath, ResourcePath itemPrefabPath);
    void ShowInventory();
}
