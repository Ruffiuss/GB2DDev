using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Data
{
    public class AbilitiesGroupView : MonoBehaviour, IAbilityCollectionView
    {
        #region Fields

        [SerializeField] private AbilityView _view;
        [SerializeField] private Transform _layout;

        private List<AbilityView> _currentViews = new List<AbilityView>();

        #endregion

        #region Properties

        public event EventHandler<IItem> UseRequested;

        #endregion

        #region ClassLifeCycles
        ~AbilitiesGroupView()
        {
            foreach (var view in _currentViews)
            {
                view.OnClick -= AbilityHandler;
            }
            _currentViews.Clear();
        }

        #endregion

        #region Methods

        public void Display(IReadOnlyList<IItem> abilityItems)
        {
            foreach (var ability in abilityItems)
            {
                var view = Instantiate<AbilityView>(_view, _layout);
                view.OnClick += AbilityHandler;
                view.Init(ability);
                _currentViews.Add(view);
            }
        }

        private void AbilityHandler(IItem obj)
        {
            UseRequested?.Invoke(this, obj);
        }

        #endregion
    }
}
