using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Transform _playerTransform;

    private bool _canMove = true;

    private void Update()
    {
        _enemyMovement.SetDestination(_playerTransform.position);
    }

    public void Init(EnemyInfo info, Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _enemyMovement.SetSpeed(info.moveSpeed);
    }
}
