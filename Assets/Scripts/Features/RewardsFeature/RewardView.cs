using Model;
using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Features.RewardsFeature
{
    public class RewardView : MonoBehaviour, IView
    {
        #region Fields

        private PlayerRewardDataHandler _dataSaver;
        private ProfilePlayer _profilePlayer;

        [Header("Daily time settings")]
        [SerializeField]
        public int DailyTimeCooldown = 5;
        [SerializeField]
        public int DailyTimeDeadline = 10;
        [SerializeField]
        public int WeeklyTimeCooldown = 10;
        [SerializeField]
        public int WeeklyTimeDeadline = 20;
        [Space]
        [Header("RewardSettings")]
        public List<Reward> DailyRewards;
        public List<Reward> WeeklyRewards;
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _diamondText;
        [SerializeField]
        private TextMeshProUGUI _woodText;
        [SerializeField]
        public Slider DailyRewardTimer;
        [SerializeField]
        public Slider WeeklyRewardTimer;
        [SerializeField]
        public Transform DailySlotsParent;
        [SerializeField]
        public Transform WeeklySlotsParent;
        [SerializeField]
        public SlotRewardView SlotPrefab;
        [SerializeField]
        public Button ResetButton;
        [SerializeField]
        public Button ExitButton;
        [SerializeField]
        public Button GetDailyRewardButton;
        [SerializeField]
        public Button GetWeeklyRewardButton;

        #endregion

        #region Properties

        private int Wood
        {
            get => _dataSaver.WoodCount;
            set => _dataSaver.WoodCount = value;
        }

        private int Diamond
        {
            get => _dataSaver.DimondCount;
            set => _dataSaver.DimondCount = value;
        }

        public int CurrentActiveSlotDaily
        {
            get => _dataSaver.ActiveSlotKeyDaily;
            set => _dataSaver.ActiveSlotKeyDaily = value;
        }

        public int CurrentActiveSlotWeekly
        {
            get => _dataSaver.ActiveSlotKeyWeekly;
            set => _dataSaver.ActiveSlotKeyWeekly = value;
        }

        public DateTime? LastRewardTime
        {
            get => _dataSaver.LastTimeKeyDaily;
            set => _dataSaver.LastTimeKeyDaily = value;
        }

        public DateTime? LastRewardTimeWeekly
        {
            get => _dataSaver.LastTimeKeyWeekly;
            set => _dataSaver.LastTimeKeyWeekly = value;
        }

        #endregion

        #region UnityMethods

        private void Start()
        {
            RefreshText();
        }

        private void OnDestroy()
        {
            GetDailyRewardButton.onClick.RemoveAllListeners();
            GetWeeklyRewardButton.onClick.RemoveAllListeners();
            ResetButton.onClick.RemoveAllListeners();
        }

        #endregion

        #region Methods

        public void InitSaver(PlayerRewardDataHandler dataSaver)
        {
            _dataSaver = dataSaver;
        }

        private void RefreshText()
        {
            if (_diamondText != null)
                _diamondText.text = Diamond.ToString();
            if (_woodText != null)
                _woodText.text = Wood.ToString();
        }

        public void AddDiamond(int count)
        {
            Diamond += count;
            RefreshText();
        }

        public void AddWood(int count)
        {
            Wood += count;
            RefreshText();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}