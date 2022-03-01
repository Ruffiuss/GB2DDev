using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using Features.AbilitiesFeature;
using Features.FightsFeature;
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
        private readonly ProfilePlayer _profilePlayer;
        private FightWindowView _fightWindow;
        private bool _isFighting = false;

        public GameController(ProfilePlayer profilePlayer, InventoryModel inventoryModel, Transform uiRoot)
        {
            _profilePlayer = profilePlayer;

            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var inGameMenu = ResourceLoader.LoadAndInstantiateView<InGameMenu>(new ResourcePath { PathResource = "Prefabs/InGameMenu" }, uiRoot);
            AddGameObjects(inGameMenu.gameObject);
            inGameMenu.FightButton.onClick.AddListener(StartFight);
            inGameMenu.ExitGame.onClick.AddListener(Escape);

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

        private void Escape()
        {
            if (_isFighting)
            {
                _profilePlayer.CurrentState.Value = GameState.Game;
                GameObject.Destroy(_fightWindow?.gameObject);
                _isFighting = false;
            }
        }

        private void StartFight()
        {
            if (!_isFighting)
            {
                _fightWindow = ResourceLoader.LoadAndInstantiateView<FightWindowView>(new ResourcePath() { PathResource = "Prefabs/FightWindowView" }, default);
                AddGameObjects(_fightWindow.gameObject);
                _profilePlayer.CurrentState.Value = GameState.Fight;
                _isFighting = true;
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}