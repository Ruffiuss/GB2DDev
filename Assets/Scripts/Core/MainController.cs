using CoreGame;
using Data;
using Features.InventoryFeature;
using Features.RewardsFeature;
using Features.ShedFeature;
using Model;
using System.Collections.Generic;
using System.Linq;
using Tools.ResourceManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class MainController : BaseController
    {
        public MainController(Transform placeForUi, ProfilePlayer profilePlayer, PlayerRewardDataHandler dataSaver, AssetReferenceGameObject mainMenuAsset)
        {
            _mainMenuAsset = mainMenuAsset;
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _dataSaver = dataSaver;

            _itemsConfig = ResourceLoader.LoadDataSource<ItemConfig>(
                new ResourcePath(){
                    PathResource = "Data/ItemsSource" })
                .ToList();

            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private MainMenuController _mainMenuController;
        private ShedController _shedController;
        private GameController _gameController;
        private InventoryController _inventoryController;
        private RewardController _rewardController;
        private readonly Transform _placeForUi;
        private readonly PlayerRewardDataHandler _dataSaver;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemsConfig;
        private readonly AssetReferenceGameObject _mainMenuAsset;

        protected override void OnDispose()
        {
            AllClear();

            _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _mainMenuAsset);
                    _shedController = new ShedController(_itemsConfig, _profilePlayer.CurrentCar);
                    _shedController.Enter();
                    _shedController.Exit();
                    _gameController?.Dispose();
                    _inventoryController?.Dispose();
                    _rewardController?.Dispose();
                    break;
                case GameState.Game:
                    var inventoryModel = new InventoryModel();
                    _inventoryController = new InventoryController(_itemsConfig, inventoryModel);
                    _inventoryController.ShowInventory();
                    _gameController = new GameController(_profilePlayer, inventoryModel, _placeForUi);
                    _mainMenuController?.Dispose();
                    break;
                case GameState.Fight:
                    break;
                case GameState.Reward:
                    _mainMenuController?.Dispose();
                    _rewardController = new RewardController(_placeForUi, _dataSaver, _profilePlayer);
                    break;
                default:
                    AllClear();
                    break;
            }
        }

        private void AllClear()
        {
            _inventoryController?.Dispose();
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
        }
    }
}