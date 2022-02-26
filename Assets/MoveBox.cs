using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    [SerializeField] private float _duration = 3.0f;
    [SerializeField] private List<Vector3> _points = new List<Vector3>();
    [SerializeField] private AnimationCurve _easing = AnimationCurve.EaseInOut(0,0,1,1);

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.OnStepComplete(() => Debug.Log("I'am here!"));
        sequence.Append(transform.DOPath(_points.ToArray(), _duration, PathType.CatmullRom));
        sequence.Insert(0, transform.DORotate(new Vector3(0, 1260,0), _duration, RotateMode.FastBeyond360));
        sequence.Append(transform.DOPath(_points.ToArray(), _duration, PathType.CatmullRom).From());
        //sequence.Insert(0.5f, transform.DOShakeScale(_duration, 1.5f, 10));
        sequence.SetLoops(10);
        //sequence.SetEase(Ease.InBounce);
        sequence.SetEase(_easing);
    }
}
