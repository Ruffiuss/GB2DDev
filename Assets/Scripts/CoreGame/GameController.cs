using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using Features.AbilitiesFeature;
using Features.InventoryFeature;
using InputFeature;
using Model;
using Tools.ResourceManagement;
using Tools.RX;
using UnityEngine;

namespace CoreGame
{
    public class GameController : BaseController
    {
        private readonly List<AbilityItemConfig> _abilityItemsConfig;

        public GameController(ProfilePlayer profilePlayer, InventoryModel inventoryModel, Transform uiRoot)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);

            _abilityItemsConfig = ResourceLoader.LoadDataSource<AbilityItemConfig>(new ResourcePath()
            { PathResource = "Data/AbilityItemsSource" }).ToList();

            var abilityRepository = new AbilityRepository(_abilityItemsConfig);

            var abilityView =
                ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                    new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);

            var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
                abilityView);
            AddController(abilitiesController);

        }
    }
}