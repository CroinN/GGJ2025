using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string AttackAnimKey = "Attack";
    
    [SerializeField] private Animator _animator;
    
    private bool _isDead = false;

    public void OnAttack()
    {
        _animator.SetTrigger(AttackAnimKey);
    }

    public void OnDie()
    {
        if (!_isDead)
        {
            _isDead = true;
            _animator.SetTrigger("Dead");
        }
    }
}
