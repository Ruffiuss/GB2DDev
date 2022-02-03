using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class FloatInputJoystick : BaseInputView
{
    #region Fields

    [SerializeField] private Joystick _joystick;
    private CanvasGroup _container;
    private Transform _placeForUI;

    private bool _usedJoystick;

    #endregion

    #region Methods

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed, Transform placeForUI)
    {
        base.Init(leftMove, rightMove, speed);
        _placeForUI = placeForUI;
        gameObject.transform.SetParent(_placeForUI);
        _container = gameObject.GetComponentInParent<CanvasGroup>();
        UpdateManager.SubscribeToUpdate(Move);
        UpdateManager.SubscribeToUpdate(TouchHandler);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
        UpdateManager.UnsubscribeFromUpdate(TouchHandler);
    }

    private void TouchHandler()
    {
        if (Input.touchCount > 0 )
        {
            var lastTouch = Input.GetTouch(0);
            switch (lastTouch.phase)
            {
                case TouchPhase.Began:
                    if (!_usedJoystick)
                        OnPointerDown(lastTouch.position);
                    break;
                case TouchPhase.Moved:
                    OnDrag(lastTouch.position);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    OnPointerUp();
                    break;
                case TouchPhase.Canceled:
                    OnPointerUp();
                    break;
                default:
                    break;
            }
        }
    }

    public void OnPointerDown(Vector3 position)
    {
        _joystick.transform.position = position;
        _joystick.SetStartPosition(position);
        _joystick.OnPointerDown();
        _usedJoystick = true;
        _container.alpha = 1;
    }

    public void OnPointerUp()
    {
        _usedJoystick = false;
        _container.alpha = 0;
    }

    public void OnDrag(Vector3 position)
    {
        _joystick.OnDrag(position);
    }

    private void Move()
    {
        if (_usedJoystick)
        {
            float moveStep = 10 * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
            if (moveStep > 0)
                OnRightMove(moveStep);
            else if (moveStep < 0)
                OnLeftMove(moveStep);
        }
    }

    #endregion
}
