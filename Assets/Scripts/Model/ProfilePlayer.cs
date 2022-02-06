using Profile;
using Shop;
using System;
using Tools;


public class ProfilePlayer
{
    #region Properties

    public SubscriptionProperty<GameState> CurrentState { get; }
    public SubscriptionProperty<ushort> CurrentGoldCount { get; }
    public Car CurrentCar { get; }

    #endregion

    #region ClassLifeCycles

    public ProfilePlayer(float speedCar)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        CurrentGoldCount = new SubscriptionProperty<ushort>();
        CurrentGoldCount.Value = (ushort)0; //need to save data about player`s gold count and sync here
    }

    ~ProfilePlayer()
    {

    }

    #endregion

    #region Methods

    public void RegisterGoldController(GoldController goldController)
    {
        goldController.OnGoldCountChange += ChangeGoldCount;
    }

    public void UnregisterGoldController(GoldController goldController)
    {
        goldController.OnGoldCountChange -= ChangeGoldCount;
    }

    private void ChangeGoldCount(ushort count)
    {
        CurrentGoldCount.Value += count;
    }

    #endregion
}

