using Features.RewardsFeature;
using Model;
using Model.Analytic;
using Tools.Ads;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private UnityAdsTools _ads;
        [SerializeField] private PlayerRewardDataHandler _dataSaver;
        [SerializeField] private AssetReferenceGameObject _mainMenuAsset;

        private MainController _mainController;
        private IAnalyticTools _analyticsTools;

        private void Awake()
        {
            _analyticsTools = new UnityAnalyticTools();
            var profilePlayer = new ProfilePlayer(15f, _ads, _analyticsTools, _dataSaver);
            _mainController = new MainController(_placeForUi, profilePlayer, _dataSaver, _mainMenuAsset);
            profilePlayer.CurrentState.Value = GameState.Start;
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}