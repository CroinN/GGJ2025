using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IService
{
    [SerializeField] private int[] _inventory = new int[4];

    public void AddItem(VendingMachine.Drink item)
    {
        _inventory[(int)item]++;
    }

    public void UseItem(VendingMachine.Drink item)
    {
        if (GetItem(item) > 0)
        {
            _inventory[(int)item]--;
        }
    }

    public int GetItem(VendingMachine.Drink item)
    {
        return _inventory[(int)item];
    }
    
    private void Awake()
    {
        RegisterService();
    }

    private void OnDestroy()
    {
        UnregisterService();
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
