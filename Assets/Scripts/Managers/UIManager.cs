using System;
using UnityEngine;

public class UIManager : MonoBehaviour, IService
{
    //TODO clean uo this mess
    [SerializeField] private GameObject _VendingText;
    public void EnableVendingText()
    {
        _VendingText.SetActive(true);   
    }
    public void DisableVendingText()
    {
        _VendingText.SetActive(false);
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
