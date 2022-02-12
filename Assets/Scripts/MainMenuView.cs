using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonAdsReward;
    [SerializeField] private Button _buttonPurchase;
    [SerializeField] private Text _goldCountText;

    private IReadOnlySubscriptionProperty<int> _goldCount;

    public void Init(UnityAction startGame, UnityAction adsReward, UnityAction buy, IReadOnlySubscriptionProperty<int> goldCount)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonAdsReward.onClick.AddListener(adsReward);
        _buttonPurchase.onClick.AddListener(buy);
        _goldCount = goldCount;
        _goldCount.SubscribeOnChange(UpdateGoldText);
    }

    private void UpdateGoldText(int count)
    {
        _goldCountText.text = count.ToString();
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