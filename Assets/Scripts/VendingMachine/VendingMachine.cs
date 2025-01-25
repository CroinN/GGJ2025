using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class VendingMachine : MonoBehaviour
{  
    public enum Drink
    {
        Cola,
        Fanta,
        Jermuk,
        Sprite
    }
    
    [Serializable]
    public struct Item
    {
        public Drink type;
        public int price;
    }

    private CurrencyManager _currencyManager;

    public List<Item> priceList;

    public int GetPrice(Drink type) => priceList.Where(obj=>obj.type==type).ToArray()[0].price;
    
    private void Start()
    {
        _currencyManager = SL.Get<CurrencyManager>();
    }

    public void Purchase(Drink type, Action<bool> callback)
    {
        int price = GetPrice(type);
        bool purchasePossible = _currencyManager.HasEnoughCurrency(price);
        callback?.Invoke(purchasePossible);

        if (purchasePossible)
        {
            _currencyManager.RemoveCurrency(price);
        }
    }
}
