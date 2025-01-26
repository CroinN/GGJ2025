using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string AttackAnimKey = "Attack";
    
    [SerializeField] private Animator _animator;

    public void OnAttack()
    {
        _animator.SetTrigger(AttackAnimKey);
    }
}
