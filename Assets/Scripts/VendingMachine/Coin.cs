using System;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotatingTime;
    [SerializeField] private int _currencyCount;

    public int GetCurrency()
    {
        return _currencyCount;
    }
    private void Start()
    {
        CoinAnimation();
    }

    private void CoinAnimation()
    {
        Vector3 rotation = Quaternion.AngleAxis(180, Vector3.up).eulerAngles;
        transform.DORotate(rotation, _rotatingTime)
             .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Incremental);
        transform.DOMoveY(1, _rotatingTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
