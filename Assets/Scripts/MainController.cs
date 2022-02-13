using Profile;
using System.Collections.Generic;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, 
        List<ItemConfig> itemsConfig, IReadOnlyList<UpgradeItemConfig> upgradeItems,
        IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        _inventoryModel = new InventoryModel();
    }

    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private InventoryModel _inventoryModel;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;

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
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _shedController = new ShedController(_inventoryModel, _upgradeItems, _itemsConfig, _profilePlayer.CurrentCar, _placeForUi);
                _shedController.Enter();
                _shedController.Exit();
                _gameController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Game:
                _shedController?.Dispose();
                _inventoryController = new InventoryController(_itemsConfig, _inventoryModel);
                _inventoryController.ShowInventory();
                _gameController = new GameController(_profilePlayer, _abilityItems, _inventoryModel, _placeForUi);
                _mainMenuController?.Dispose();
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
