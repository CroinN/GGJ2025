using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class VendingMachine : MonoBehaviour
{  
    public enum Drink
    {
        HayKola,
        BariNarinj,
        SarainDyu
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
    
    [SerializeField] private Drink _selectedDrink;

    private void Start()
    {
        _currencyManager = SL.Get<CurrencyManager>();
    }

    public void SelectDrink(Drink type)
    {
        _selectedDrink = type;
    }

    public void Purchase(Drink type)
    {
        int price = GetPrice(type);
        
        if (_currencyManager.HasEnoughCurrency(price))
        {
            _currencyManager.RemoveCurrency(price);
        }
        else
        {
            // Notify that is not complete
        }
    }
}
