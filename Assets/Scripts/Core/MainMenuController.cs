﻿using System.Collections.Generic;
using Model;
using Tools.ResourceManagement;
using UnityEngine;

namespace Core
{
    public class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            AddGameObjects(_view.gameObject);
            _view.Init(StartGame, ViewRewards, ExitGame);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            return ResourceLoader.LoadAndInstantiateView<MainMenuView>(_viewPath, placeForUi);
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;

            _profilePlayer.AnalyticTools.SendMessage("start_game",
                new Dictionary<string, object>(){
                    {"time", Time.realtimeSinceStartup }
                });
        }

        private void ViewRewards()
        {
            _profilePlayer.CurrentState.Value = GameState.Reward;
        }


        private void ExitGame()
        {

        }
    }
}