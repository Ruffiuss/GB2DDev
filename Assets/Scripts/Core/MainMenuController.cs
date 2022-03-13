using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core
{
    public class MainMenuController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly AssetReferenceGameObject _viewAsset;
        private MainMenuView _view;
        private AsyncOperationHandle<GameObject> _viewHandler;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, AssetReferenceGameObject asset)
        {
            _viewAsset = asset;
            _viewHandler = Addressables.InstantiateAsync(_viewAsset, placeForUi);
            _viewHandler.Completed += InitView;

            _profilePlayer = profilePlayer;
        }

        private void InitView(AsyncOperationHandle<GameObject> obj)
        {
            _view = obj.Result.GetComponent<MainMenuView>();
            AddGameObjects(_view.gameObject);
            _view.Init(StartGame, ViewRewards, ExitGame);
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
            Application.Quit();
        }
    }
}