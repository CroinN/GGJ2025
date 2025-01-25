using System;
using UnityEngine;

public class GarbageManager : MonoBehaviour, IService
{
    [SerializeField] private Transform _garbageParent;

    public Transform garbageParent => _garbageParent;


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
        SL.Register(this);
    }
}
