using Profile;
using Tools;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        Gold = new SubscriptionProperty<int>();
        CurrentCar = new Car(speedCar);
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public SubscriptionProperty<int> Gold { get; }

    public Car CurrentCar { get; }
}

