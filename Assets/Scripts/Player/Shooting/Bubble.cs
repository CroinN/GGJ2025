using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bubble : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private LayerMask _popingLayerMask;
    [SerializeField] private float _duration;
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _speedOffset;
    
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _timeElapsed;
    private int _damage;

    public int Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 direction, int damage)
    {
        _direction = direction;
        _damage = damage;
        _initialSpeed += Random.Range(-_speedOffset, _speedOffset);
        Destroy(gameObject, _duration+1);
    }

    private void Pop()
    {
        Instantiate(_particle, transform.position, Quaternion.identity, SL.Get<GarbageManager>().garbageParent);
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        _rigidbody.velocity += _direction * (_curve.Evaluate(Mathf.Clamp01(_timeElapsed/_duration)) * _initialSpeed * Time.deltaTime);
    }
    
    private void OnDestroy()
    {
        Pop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( (_popingLayerMask & (1 << other.gameObject.layer)) == 0)
        {
            Destroy(gameObject);
        }
    }
}
