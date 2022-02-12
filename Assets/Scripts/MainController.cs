using Model.Analytic;
using Profile;
using Shop;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analyticTools, IAdsShower ads, IShop shop, List<ShopProduct> products)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _analyticTools = analyticTools;
        _ads = ads;
        _shop = shop;

        _purchaceController = new PurchaseController(_shop, _profilePlayer, products);
        AddController(_purchaceController);

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly PurchaseController _purchaceController;
    private readonly IAnalyticTools _analyticTools;
    private readonly IAdsShower _ads;
    private readonly IShop _shop;

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticTools, _ads, _shop);
                _analyticTools.SendMessage("MainMenuOpened");
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _analyticTools.SendMessage("PlayerGameStarted");
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
