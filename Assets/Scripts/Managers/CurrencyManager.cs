using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IService
{
    [SerializeField] private int _currencyCount;


    private void Awake()
    {
        RegisterService();
    }

    public void AddCurrency(int count)
    {
        _currencyCount += count;
    }

    public void RemoveCurrency(int count)
    {
        if(HasEnoughCurrency(count))
        {
            _currencyCount -= count;
        }
    }

    public bool HasEnoughCurrency(int count)
    {
        bool hasEnoughCurrency = count <= _currencyCount;
        return hasEnoughCurrency;
    }
    
    
    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }
}
