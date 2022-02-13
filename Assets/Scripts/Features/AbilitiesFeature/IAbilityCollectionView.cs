using System;
using System.Collections.Generic;
using UI;

public interface IAbilityCollectionView: IView
{
    event EventHandler<IItem> UseRequested;
    void Display(IReadOnlyList<IItem> abilityItems);
}