using System;
using UnityEngine;

public class PlayerHealthContoller : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 150;
    
    private PlayerInfoManager _playerInfoManager;
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;   
    }

    private void Start()
    {
        _playerInfoManager = SL.Get<PlayerInfoManager>();
        _playerInfoManager.UpdateHealth(1);
    }

    public void GetHeal(int point)
    {
        _health += point;
        Mathf.Clamp(_health, 0, _maxHealth);

        _playerInfoManager.UpdateHealth((float)_health / (float)_maxHealth);
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(damage, 0, _maxHealth);
        
        if (_health <= 0)
        {
            OnDie();
        }
        _playerInfoManager.UpdateHealth((float)_health/(float)_maxHealth);
    }

    private void OnDie()
    {
        
    }
}
