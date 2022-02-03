using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class FloatInputJoystick : BaseInputView, IPointerDownHandler, IPointerUpHandler, IDragHandler
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
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _joystick.transform.position = eventData.position;
        _joystick.SetStartPosition(eventData.position);
        _joystick.OnPointerDown(eventData);
        _usedJoystick = true;
        _container.alpha = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _usedJoystick = false;
        _container.alpha = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _joystick.OnDrag(eventData);
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
