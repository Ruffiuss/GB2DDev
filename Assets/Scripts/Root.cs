using Model.Analytic;
using Profile;
using Shop;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour, IAnalyticTools
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsTools _unityAdsTools;
    [SerializeField] private List<ShopProduct> _shopProducts;

    private MainController _mainController;
    private ShopTools _shopTools;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f);
        profilePlayer.CurrentState.Value = GameState.Start;
        _shopTools = new ShopTools(_shopProducts);
        _mainController = new MainController(_placeForUi, profilePlayer, this, _unityAdsTools, _shopTools, _shopProducts);
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
