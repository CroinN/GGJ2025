using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController _enemyAnimationController;
    [SerializeField] private EnemyAttackController _enemyAttackController;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _fireMaterial;
    [SerializeField] private Material _unfireMaterial;

    private int _health = 100;
    private bool _isDead = false;

    private Tween _tween;
    private Tween _duration;
    private bool _isBurning;

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

    public void FireEffect()
    {
        if (!_isBurning)
        {
            _isBurning = true;

            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _skinnedMeshRenderer.material = _fireMaterial)
                    .AppendInterval(0.25f)
                    .AppendCallback(() => _skinnedMeshRenderer.material = _unfireMaterial)
                    .AppendInterval(0.25f)
                    .SetLoops(-1, LoopType.Restart);
            _tween = sequence;
        }

        _duration.Kill();
        _duration = DOVirtual.DelayedCall(5, () =>
        {
            _tween.Kill();
            _skinnedMeshRenderer.material = _unfireMaterial;
            _isBurning = false;
        });
    }
}
