using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackDistance;
    
    private bool _isAttackInCooldown = false;
    
    private void Update()
    {
        if (!_isAttackInCooldown && Vector3.Distance(_playerTransform.position, _navMeshAgent.transform.position) <= _attackDistance)
        {  
            AttackCooldown();
            _enemyAnimationController.OnAttack();
        }
    }

    private void AttackCooldown()
    {
        _isAttackInCooldown = true;
        _navMeshAgent.enabled = false;
        DOVirtual.DelayedCall(_attackCooldown, () =>
        {
            _navMeshAgent.enabled = true;
            _isAttackInCooldown = false;
        });
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
}
