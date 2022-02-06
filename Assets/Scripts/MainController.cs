using Model.Analytic;
using Model.Shop;
using Profile;
using Shop;
using Tools.Ads;
using UnityEngine;


public class MainController : BaseController
{
    #region Fields

    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly ShopTools _shop;
    private readonly IAnalyticTools _analyticTools;
    private readonly IAdsShower _ads;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private GoldController _goldController;

    #endregion

    #region ClassLifeCycles

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analyticTools, IAdsShower ads, ShopTools shop)
    {
        _shop = shop;
        _goldController = new GoldController(profilePlayer, _shop);
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _analyticTools = analyticTools;
        _ads = ads;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        _goldController?.Dispose();
        base.OnDispose();
    }

    #endregion

    #region Methods

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _analyticTools, _ads, _goldController.OnViewLoaded, _shop.OnButtonRegister);
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

    #endregion
}
