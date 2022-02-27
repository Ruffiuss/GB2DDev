using System;
using System.IO;
using Tools.RX;
using UnityEngine;

namespace Features.RewardsFeature
{
    public class PlayerRewardDataHandler : MonoBehaviour
    {
        #region Fields

        public readonly string SaveFolder = @"/Saves/";
        public readonly string SaveFileName = @"PlayerData.json";

        private PlayerRewardData _currentPlayerRewardData;

        #endregion

        #region Properties

        public int WoodCount
        {
            get => _currentPlayerRewardData.WoodCount.Value;
            set => _currentPlayerRewardData.WoodCount.Value = value;
        }

        public int DimondCount
        {
            get => _currentPlayerRewardData.DiamondCount.Value;
            set => _currentPlayerRewardData.DiamondCount.Value = value;
        }

        public int ActiveSlotKeyDaily
        {
            get => _currentPlayerRewardData.ActiveSlotKeyDaily.Value;
            set => _currentPlayerRewardData.ActiveSlotKeyDaily.Value = value;
        }

        public int ActiveSlotKeyWeekly
        {
            get => _currentPlayerRewardData.ActiveSlotKeyWeekly.Value;
            set => _currentPlayerRewardData.ActiveSlotKeyWeekly.Value = value;
        }

        public DateTime? LastTimeKeyDaily
        {
            get
            {
                if (!_currentPlayerRewardData.LastTimeKeyDaily.Value.HasValue)
                    return null;
                return _currentPlayerRewardData.LastTimeKeyDaily.Value;
            }
            set
            {
                if (value != null)
                    _currentPlayerRewardData.LastTimeKeyDaily.Value = value;
                else
                    _currentPlayerRewardData.LastTimeKeyDaily.Value = default;
            }
        }

        public DateTime? LastTimeKeyWeekly
        {
            get
            {
                if (_currentPlayerRewardData.LastTimeKeyWeekly.Value.HasValue)
                    return null;
                return _currentPlayerRewardData.LastTimeKeyWeekly.Value;
            }
            set
            {
                if (value != null)
                    _currentPlayerRewardData.LastTimeKeyWeekly.Value = value;
                else
                    _currentPlayerRewardData.LastTimeKeyWeekly.Value = default;
            }
        }

        #endregion

        #region UnityMethods

        private void Awake()
        {
            if (!Directory.Exists(Application.dataPath + SaveFolder))
            {
                Directory.CreateDirectory(Application.dataPath + SaveFolder);
                File.Create(Application.dataPath + SaveFolder + SaveFileName);
                _currentPlayerRewardData = new PlayerRewardData();
            }
            else
                _currentPlayerRewardData = LoadData();

        }

        private void LateUpdate()
        {
            if (Time.frameCount % 200 == 0)
                SaveData();
        }

        #endregion

        #region Methods

        public void SaveData()
        {
            var jsonString = JsonUtility.ToJson(_currentPlayerRewardData);
            var jsonString2 = JsonUtility.ToJson(new SubscriptionProperty<int>());
            File.WriteAllText(Application.dataPath + SaveFolder + SaveFileName, jsonString);
        }

        public PlayerRewardData LoadData()
        {
            if (_currentPlayerRewardData is null)
            {
                var jsonString = File.ReadAllText(Application.dataPath + SaveFolder + SaveFileName);
                var data = JsonUtility.FromJson<PlayerRewardData>(jsonString);
                if (data is null)
                    return InitDefault();
                else return data;
            }
            else return _currentPlayerRewardData;
        }

        private PlayerRewardData InitDefault()
        {
            return new PlayerRewardData
            {
                LastTimeKeyDaily = default,
                LastTimeKeyWeekly = default,
                ActiveSlotKeyDaily = default,
                ActiveSlotKeyWeekly = default,
                WoodCount = default,
                DiamondCount = default
            };
        }

        #endregion
    }
}
