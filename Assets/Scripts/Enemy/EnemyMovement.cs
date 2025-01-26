using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public void SetSpeed(float speed)
    {
        _navMeshAgent.speed = speed;
    }
    
    public void SetDestination(Vector3 destination)
    {
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.destination = destination;
        }
    }
}
