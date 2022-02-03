using Tools;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, UnityEngine.Transform placeForUI)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, placeForUI);
        AddController(inputGameController);
            
        var carController = new CarController();
        AddController(carController);
    }
}

