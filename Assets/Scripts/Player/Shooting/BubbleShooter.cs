using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleShooter : MonoBehaviour
{
    [SerializeField] private float _startOffsetRadius;
    [SerializeField] private float _endOffsetRadius;

    [SerializeField] private float _bubblesPerSecond;
    
    [SerializeField] private Transform _shootingStartPoint;
    [SerializeField] private Transform _shootingEndPoint;
    [SerializeField] private Bubble _bubblePrefab;
    [SerializeField] private int _damage;

    private InventoryManager _inventoryManager;
    private bool _isInCooldown = false;

    private void Start()
    {
        _inventoryManager = SL.Get<InventoryManager>();
    }

    private void Update()
    {
        HandleAmmoSwitching();
    }

    private void HandleAmmoSwitching()
    {
        int index = -1;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            index = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            index = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            index = 3;
        }
        
        if(index != -1 && _inventoryManager.GetItem((VendingMachine.Drink)index)>0)
        {
            _inventoryManager.UseItem((VendingMachine.Drink)index);
        }
    }

    public void Shoot()
    {
        if (!_isInCooldown)
        {
            StartCooldown();
            Vector3 start = GetRandomPositionInCircle(_shootingStartPoint.position, _startOffsetRadius, transform.forward);
            Vector3 end = GetRandomPositionInCircle(_shootingEndPoint.position, _endOffsetRadius, transform.forward);

            Bubble bubble = Instantiate(_bubblePrefab, start, Quaternion.identity, 
                SL.Get<GarbageManager>().garbageParent);
            bubble.Init((end - start).normalized, _damage);
        }
    }

    private void StartCooldown()
    {
        _isInCooldown = true;
        float cooldownTime = 1 / _bubblesPerSecond;
        DOVirtual.DelayedCall(cooldownTime, () => { _isInCooldown = false; });
    }

    private void OnDrawGizmos()
    {
        Vector3 normal = transform.forward;

        Handles.DrawWireArc(_shootingStartPoint.position, normal, transform.up, 360, _startOffsetRadius);
        Handles.DrawWireArc(_shootingEndPoint.position, normal, transform.up, 360, _endOffsetRadius);

        int lineCount = 36;

        Vector3 tangent = Vector3.Cross(normal, Vector3.right).normalized;
        
        Vector3 bitangent = Vector3.Cross(normal, tangent);

        for (int i = 0; i < lineCount; i++)
        {
            float angle = (i / (float)lineCount) * Mathf.PI * 2.0f;

            Vector3 offset = (tangent * Mathf.Cos(angle) + bitangent * Mathf.Sin(angle));
            Vector3 from = _shootingStartPoint.position + offset*_startOffsetRadius;
            Vector3 to = _shootingEndPoint.position + offset*_endOffsetRadius;
            Gizmos.DrawLine(from, to);
        }
    }
  
    public static Vector3 GetRandomPositionInCircle(Vector3 center, float radius, Vector3 normal)
    {
        Vector3 unitNormal = normal.normalized;

        float theta = Random.Range(0f, Mathf.PI * 2f);
        float r = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        Vector3 point2d = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 0);

        Vector3 tangent = Vector3.Cross(unitNormal, Vector3.right).normalized;
        if (tangent.magnitude < 0.001f)
        {
            tangent = Vector3.Cross(unitNormal, Vector3.up).normalized;
        }
        Vector3 bitangent = Vector3.Cross(unitNormal, tangent);

        Vector3 point = center + tangent * point2d.x + bitangent * point2d.y;

        return point;
    }
}
