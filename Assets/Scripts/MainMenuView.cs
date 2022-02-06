using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonAdsReward;

    public void Init(UnityAction startGame, UnityAction adsReward)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonAdsReward.onClick.AddListener(adsReward);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonAdsReward.onClick.RemoveAllListeners();
    }

    public void DisableAdsRewardButton()
    {
        _buttonAdsReward.interactable = false;
    }
}