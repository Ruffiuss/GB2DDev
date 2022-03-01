using Features.RewardsFeature;
using Model.Analytic;
using Tools.Ads;
using Tools.RX;

namespace Model
{
    public class ProfilePlayer
    {
        private readonly PlayerRewardDataHandler _dataSaver;

        public ProfilePlayer(float speedCar, IAdsShower adsShower, IAnalyticTools analyticTools, PlayerRewardDataHandler dataSaver)
        {
            _dataSaver = dataSaver;
            InitRewardData();

            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new Car(speedCar);
            AdsShower = adsShower;
            AnalyticTools = analyticTools;
        }

        private void InitRewardData()
        {
            RewardData = _dataSaver.LoadData();
        }

        public IAdsShower AdsShower { get; }

        public IAnalyticTools AnalyticTools { get; }

        public SubscriptionProperty<GameState> CurrentState { get; }

        public Car CurrentCar { get; }

        public PlayerRewardData RewardData { get; private set; }
    }
}