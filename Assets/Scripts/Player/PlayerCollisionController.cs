using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private const string AttackHitboxTag = "EnemyAttackHitbox";
    
    [SerializeField] private PlayerHealthContoller _playerHealthContoller;
    
    private int _damage = 15;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(AttackHitboxTag))
        {
            _playerHealthContoller.GetDamage(_damage);
        }
    }
}
