using Model.Analytic;
using Model.Shop;
using Profile;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour, IAnalyticTools
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsTools _unityAdsTools;
    [SerializeField] private List<ShopProduct> _products;

    private ShopTools _shopTools;
    private MainController _mainController;

    private void Awake()
    {
        _shopTools = new ShopTools(_products);
        var profilePlayer = new ProfilePlayer(15f);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, this, _unityAdsTools, _shopTools);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }

    public void SendMessage(string alias, IDictionary<string, object> eventData)
    {
        if (eventData == null)
            eventData = new Dictionary<string, object>();
        UnityEngine.Analytics.Analytics.CustomEvent(alias, eventData);
    }
}
