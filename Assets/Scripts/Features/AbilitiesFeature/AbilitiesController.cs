using System;
using Core;
using Features.InventoryFeature;
using ItemFeature;
using JetBrains.Annotations;
using Tools;

namespace Features.AbilitiesFeature
{
    public class AbilitiesController : BaseController
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IRepository<int, IAbility> _abilityRepository;
        private readonly IAbilityCollectionView _abilityCollectionView;
        private readonly IAbilityActivator _abilityActivator;

        public AbilitiesController(
            [NotNull] IAbilityActivator abilityActivator,
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IRepository<int, IAbility> abilityRepository,
            [NotNull] IAbilityCollectionView abilityCollectionView)
        {
            _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            _abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
            _abilityCollectionView =
                abilityCollectionView ?? throw new ArgumentNullException(nameof(abilityCollectionView));
            _abilityCollectionView.UseRequested += OnAbilityUseRequested;
            _abilityCollectionView.Display(_inventoryModel.GetEquippedItems());
        }

        private void OnAbilityUseRequested(object sender, IItem e)
        {
            if (_abilityRepository.Content.TryGetValue(e.Id, out var ability))
                ability.Apply(_abilityActivator);
        }
    }
}