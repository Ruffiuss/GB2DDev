using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryModel : IInventoryModel
{
    private readonly Dictionary<IItem, bool> _items = new Dictionary<IItem, bool>();

    public bool IsInShed { get; set; }

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        List<IItem> items = new List<IItem>();
        foreach (var item in _items)
        {
            if (item.Value)
                items.Add(item.Key);
        }
        return items;
    }

    public IReadOnlyDictionary<IItem, bool> GetAllItems()
    {
        return _items;
    }

    public void EquipItem(IItem item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] = true;
            Debug.Log($"{item.Info.Title} isEquiped {_items[item]}");
        }
        else
        {
            foreach (var item2 in _items)
            {
                if (item2.Key.Id == item.Id)
                    return;
            }
            _items.Add(item, true);
        }
    }

    public void UnEquipItem(IItem item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] = false;
            Debug.Log($"{item.Info.Title} isEquiped {_items[item]}");
        }
    }
}
