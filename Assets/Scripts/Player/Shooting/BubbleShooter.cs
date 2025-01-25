using System;
using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BubbleShooter : MonoBehaviour
{
    [SerializeField] private float _startOffsetRadius;
    [SerializeField] private float _endOffsetRadius;
    [SerializeField] private float _offsetLenght;

    [SerializeField] private float _bubblesPerSecond;
    
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bubble _bubblePrefab;

    private bool _isInCooldown = false;

    public void Shoot()
    {
        if (!_isInCooldown)
        {
            StartCooldown();
            Vector3 start = GetRandomPositionInCircle(_shootingPoint.position, _startOffsetRadius);
            Vector3 end = GetRandomPositionInCircle(_shootingPoint.position, _endOffsetRadius);
            end += new Vector3(0, 0, _offsetLenght);

            Bubble bubble = Instantiate(_bubblePrefab, start, Quaternion.identity, 
                SL.Get<GarbageManager>().garbageParent);
            bubble.Init((end - start).normalized);
        }
    }

    private void StartCooldown()
    {
        _isInCooldown = true;
        float cooldownTime = 1 / _bubblesPerSecond;
        DOVirtual.DelayedCall(cooldownTime, () => { _isInCooldown = false; });
    }

    private void OnDrawGizmosSelected()
    {
        Handles.DrawWireArc(_shootingPoint.position, Vector3.forward, Vector3.up, 360, _startOffsetRadius);
        Handles.DrawWireArc(_shootingPoint.position + new Vector3(0,0,_offsetLenght), Vector3.forward, Vector3.up, 360, _endOffsetRadius);

        int lineCount = 12;
        for (int i = 0; i < lineCount; i++)
        {
            float angle = (i / (float)lineCount) * Mathf.PI * 2f;

            Vector3 circle = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            
            Vector3 from = _shootingPoint.position + circle * _startOffsetRadius;
            Vector3 to = _shootingPoint.position + new Vector3(0,0,_offsetLenght) + circle * _endOffsetRadius;

            Gizmos.DrawLine(from, to);
        }
    }
    
    public static Vector3 GetRandomPositionInCircle(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        return center + new Vector3(x, y, 0);
    }
}
