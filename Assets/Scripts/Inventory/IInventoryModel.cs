using System.Collections.Generic;

public interface IInventoryModel
{
    bool IsInShed { get; set; }
    IReadOnlyList<IItem> GetEquippedItems();
    IReadOnlyDictionary<IItem, bool> GetAllItems();
    void EquipItem(IItem item);
    void UnEquipItem(IItem item);
}
