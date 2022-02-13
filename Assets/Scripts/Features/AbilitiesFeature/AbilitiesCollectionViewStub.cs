using ItemFeature;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitiesFeature
{
    public class AbilitiesCollectionViewStub : IAbilityCollectionView
    {
        public event EventHandler<IItem> UseRequested;
        public void Display(IReadOnlyList<IItem> abilityItems)
        {
            foreach (var item in abilityItems)
            {
                Debug.Log($"Equiped item : {item.Id}");
                UseRequested?.Invoke(this, item);
            }
        }

        public void Show()
        {
            //throw new NotImplementedException();
        }

        public void Hide()
        {
            //throw new NotImplementedException();
        }
    }
}