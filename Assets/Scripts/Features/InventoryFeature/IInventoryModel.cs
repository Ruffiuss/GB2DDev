using ItemFeature;
using System.Collections.Generic;

namespace Features.InventoryFeature
{
    public interface IInventoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void EquipItem(IItem item);
        void UnEquipItem(IItem item);
    }
}