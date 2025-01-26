using System;
using System.Collections;
using UnityEngine;

public class PlayerCurrencyController : MonoBehaviour
{
    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = SL.Get<CurrencyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            _currencyManager.AddCurrency(coin.GetCurrency());
            Destroy(other.gameObject);
        }
    }
}
