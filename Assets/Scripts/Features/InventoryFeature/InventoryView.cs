using ItemFeature;
using System.Collections.Generic;
using UnityEngine;

namespace Features.InventoryFeature
{
    public class InventoryView : IInventoryView
    {
        public void Display(IReadOnlyList<IItem> items)
        {
            foreach (var item in items)
                Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }
    }
}