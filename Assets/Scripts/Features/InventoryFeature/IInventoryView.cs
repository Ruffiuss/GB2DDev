using ItemFeature;
using System.Collections.Generic;
using UI;

namespace Features.InventoryFeature
{
    public interface IInventoryView : IView
    {
        void Display(IReadOnlyList<IItem> items);
    }
}