using UnityEngine;

public class PlayerHealthContoller : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 150;
    
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;   
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        Mathf.Clamp(damage, 0, _health);
        
        if (_health <= 0)
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        
    }
}
