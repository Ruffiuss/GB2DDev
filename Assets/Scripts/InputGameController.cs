using Tools;
using UnityEngine;

public class InputGameController : BaseController
{
    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car, Transform placeForUI)
    {
        _view = LoadView();
        _view.Init(leftMove, rightMove, car.Speed, placeForUI);
    }

    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/FloatInputJoystick" };
    private BaseInputView _view;

    private BaseInputView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(objView);
        
        return objView.GetComponent<BaseInputView>();
    }
}

