using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField]
    private Button _close;

    [SerializeField] private RectTransform _windowContainer;
    [SerializeField] private Image _background;


    private float _duration = 2f;

    void Awake()
    {
        _background.color = new Color(0, 0, 0, 0);
        _close.onClick.AddListener(Hide);
    }

    public void Show()
    {
        this._windowContainer.localScale = Vector3.zero;
        var seq = DOTween.Sequence();
        seq.Append(_windowContainer.DOScale(Vector3.one, _duration));
        seq.Insert(0, _background.DOColor(new Color(0, 0, 0, 0.3f), _duration));
    }

    public void Hide()
    {
        var seq = DOTween.Sequence();
        seq.Append(_windowContainer.DOScale(Vector3.zero, _duration));
        seq.Insert(0, _background.DOColor(new Color(0, 0, 0, 0.0f), _duration));
    }
}