using ItemFeature;
using System.Collections.Generic;

namespace Features.InventoryFeature
{
    public class InventoryModel : IInventoryModel
    {
        private readonly List<IItem> _items = new List<IItem>();

        public IReadOnlyList<IItem> GetEquippedItems()
        {
            return _items;
        }

        public void EquipItem(IItem item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
        }

        public void UnEquipItem(IItem item)
        {
            _items.Remove(item);
        }
    }
}