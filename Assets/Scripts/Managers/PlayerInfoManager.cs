using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour, IService
{
   [SerializeField] private Slider _healthbar;
   [SerializeField] private Slider _ammobar;
   [SerializeField] private Transform _playerTransform;
   
   public Transform PlayerTransform{ get => _playerTransform; set => _playerTransform = value; }
   
   public void UpdateHealth(float healthFactor)
   {
      _healthbar.value = healthFactor;
   }

   public void UpdateAmmo(float ammoFactor)
   {
      _ammobar.value = ammoFactor;  
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
