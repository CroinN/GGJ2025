using System;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour, IService
{
   [SerializeField] private Transform _playerTransform;
   
   public Transform PlayerTransform{ get => _playerTransform; set => _playerTransform = value; }

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
