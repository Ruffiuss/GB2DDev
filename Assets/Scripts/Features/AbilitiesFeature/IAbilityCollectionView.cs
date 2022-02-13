using ItemFeature;
using System;
using System.Collections.Generic;
using UI;

namespace Features.AbilitiesFeature
{
    public interface IAbilityCollectionView : IView
    {
        event EventHandler<IItem> UseRequested;
        void Display(IReadOnlyList<IItem> abilityItems);
    }
}