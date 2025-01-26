using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    [SerializeField] private EnemyAttackController _enemyAttackController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private int _health = 100;
    private bool _isDead = false;

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
        if (!_isDead)
        {
            _isDead = true;
            _navMeshAgent.enabled = false;
            _enemyAnimationController.OnDie();
            _enemyAttackController.OnDie();
            DOVirtual.DelayedCall(5, () => Destroy(gameObject));
        }
    }

    public void SetHealth(int health)
    {
        _health = health;
    }
}
