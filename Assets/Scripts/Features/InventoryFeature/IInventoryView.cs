using System.Collections.Generic;
using UI;

public interface IInventoryView:IView
{
    void Display(IReadOnlyList<IItem> items);
}
