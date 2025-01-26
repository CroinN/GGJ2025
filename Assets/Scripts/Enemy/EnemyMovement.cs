using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _freezeMaterial;
    [SerializeField] private Material _unfreezeMaterial;

    private Tween _tween;
    private bool _isFrozen;
    
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

    public void FreezeEffect()
    {
        if (!_isFrozen)
        {
            _isFrozen = true;
            _skinnedMeshRenderer.material = _freezeMaterial;
            _navMeshAgent.speed /= 2;
        }

        _tween.Kill();
        _tween = DOVirtual.DelayedCall(5, () =>
        {
            _skinnedMeshRenderer.material = _unfreezeMaterial;
            _navMeshAgent.speed *= 2;
            _isFrozen = false;
        });
    }
}
