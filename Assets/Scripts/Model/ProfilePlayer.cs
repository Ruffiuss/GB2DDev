using Model.Analytic;
using Profile;
using Tools;
using Tools.Ads;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar, IAdsShower adsShower, IAnalyticTools analyticTools)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        AdsShower = adsShower;
        AnalyticTools = analyticTools;
    }

    public IAdsShower AdsShower { get; }

    public IAnalyticTools AnalyticTools { get; }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
}

