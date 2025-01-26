using System;
using DG.Tweening;
using UnityEngine;

public class TrainController : MonoBehaviour, IService
{
    [SerializeField] private Transform _fromPos;
    [SerializeField] private Transform _toPos;
    [SerializeField] private Transform _atPos;

    [SerializeField] private DoorAnimation _doorAnimation;
    
    public void Arrive(Action callback)
    {
        transform.position = _fromPos.position;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_atPos.position, 3).SetEase(Ease.InOutCubic));
        sequence.AppendCallback(()=>OpenDoors(callback));
        
    }

    public void Leave()
    {
        CloseDoors(() =>
            {
                transform.DOMove(_toPos.position, 3)
                    .SetEase(Ease.InOutCubic);
            }
        );
    }

    public void OpenDoors(Action callback=null) => _doorAnimation.OpenDoors(callback);
    public void CloseDoors(Action callback=null) => _doorAnimation.CloseDoors(callback);

    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }

    private void Awake()
    {
        RegisterService();
    }

    private void OnDestroy()
    {
        UnregisterService();
    }
}
