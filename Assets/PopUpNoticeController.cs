using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class PopUpNoticeController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _view;
    [SerializeField] private Button _buttonNotice;

    private Queue<PopUpNoticeView> _activeNotices = new Queue<PopUpNoticeView>();
    private Queue<PopUpNoticeView> _inactiveNotices = new Queue<PopUpNoticeView>();

    private readonly Vector3 _scaleVector = new Vector3(1, 1, 0);
    private readonly float _noticeShowTime = 5.0f;

    #endregion

    #region Properties

    public string NoticeText { get; set; }

    private PopUpNoticeView PoupUpNotice
    {
        get
        {
            if (_inactiveNotices.Count > 0)
                return _inactiveNotices.Dequeue();
            else return LoadNotice();
        }
        set
        {
            _inactiveNotices.Enqueue(value);
        }
    }

    #endregion

    #region UnityMethods

    private void Awake()
    {
        if (_inactiveNotices.Count <= 0)
            _inactiveNotices.Enqueue(LoadNotice());

        NoticeText = "New notice";

        _buttonNotice.onClick.AddListener(Show);
    }

    #endregion

    #region Methods

    private PopUpNoticeView LoadNotice()
    {
        var newPopUp = GameObject.Instantiate(_view);
        newPopUp.SetActive(false);
        newPopUp.transform.SetParent(transform);
        return newPopUp.GetComponent<PopUpNoticeView>();
    }

    public void Show()
    {
        var activeNotice = PoupUpNotice;
        activeNotice.Text.text = NoticeText;
        activeNotice.Init();
        _activeNotices.Enqueue(activeNotice);
        var sequence = DOTween.Sequence();
        sequence.Append(
            activeNotice.Container.DOScale(_scaleVector, 0.5f)
            .OnComplete(Hide));
        sequence.Insert(0, activeNotice.Image.DOFade(1, 1));
    }

    private void Hide()
    {
        var sequence = DOTween.Sequence();
        var notice = _activeNotices.Dequeue();
        sequence.Append(notice.Container.DOShakeRotation(_noticeShowTime, 3.0f, 2));
        notice.Container.pivot = new Vector2(0.5f, 0.5f);
        sequence.Append(notice.Container.DOScale(Vector3.zero, 0.5f))
            .OnComplete(() => { GameObject.Destroy(notice.gameObject); });
        sequence.Insert(_noticeShowTime, notice.Text.DOFade(0, 0.5f));
        sequence.Insert(_noticeShowTime, notice.Image.DOFade(0, 0.5f));
    }
    #endregion
}
