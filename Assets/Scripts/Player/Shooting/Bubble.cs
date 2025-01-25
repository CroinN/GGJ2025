using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedOffset;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private ParticleSystem particle;
    
    private Vector3 _direction;
    private float _timeElapsed;
    
    public void Init(Vector3 direction)
    {
        _direction = direction;
        initialSpeed += Random.Range(-speedOffset, speedOffset);
        StartCoroutine(Pop());
    }

    IEnumerator Pop()
    {
        yield return new WaitForSeconds(duration);
        
        Instantiate(particle, transform.position, Quaternion.identity, SL.Get<GarbageManager>().garbageParent);
        Destroy(gameObject);
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        
        transform.position += _direction * (curve.Evaluate(Mathf.Clamp01(_timeElapsed/duration)) * initialSpeed * Time.deltaTime);
    }
}
