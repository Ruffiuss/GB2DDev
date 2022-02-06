﻿using Model.Analytic;
using Model.Shop;
using Profile;
using Shop;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;
using UnityEngine.Purchasing;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;

    private System.Action _succsessAdsShow;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics, IAdsShower ads, System.Action<GoldBalanceView> goldBalanceView, System.Action<IAPButton> iapView)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUi);
        goldBalanceView.Invoke(_view.GetComponentInChildren<GoldBalanceView>());
        iapView.Invoke(_view.GetComponentInChildren<IAPButton>());
        _view.Init(StartGame, AdsReward);
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

        _ads.ShowInterstitial();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    private void AdsReward()
    {
        _view.DisableAdsRewardButton();
        _ads.ShowReward(_succsessAdsShow);
    }
}

