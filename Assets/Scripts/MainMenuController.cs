using Model.Analytic;
using Profile;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly IAnalyticTools _analytics;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        try
        {
            _analytics.SendMessage("Start", new Dictionary<string, object>() { { "Car", _profilePlayer.CurrentCar } });
        }
        catch (System.ArgumentException e)
        {
            Debug.Log($"Error catched: {e.Message}");
        }
        finally
        {
            _analytics.SendMessage("Start with exception");
        }
        
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

