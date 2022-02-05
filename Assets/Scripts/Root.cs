using Model.Analytic;
using Profile;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour, IAnalyticTools
{
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, (IAnalyticTools)this);
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
