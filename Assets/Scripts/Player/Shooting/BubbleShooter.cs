using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleShooter : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private Transform _bottles;
    [SerializeField] private float _delayReloadMid;
    [SerializeField] private float _delayReloadFull;

    [SerializeField] private PlayerHealthContoller _playerHealthContoller;
    [SerializeField] private float _delayHealingMid;
    [SerializeField] private float _delayHealingMidd;
    [SerializeField] private float _delayHealingFull;

    [SerializeField] private float _startOffsetRadius;
    [SerializeField] private float _endOffsetRadius;

    [SerializeField] private float _bubblesPerSecond;
    
    [SerializeField] private Transform _shootingStartPoint;
    [SerializeField] private Transform _shootingEndPoint;
    [SerializeField] private int _damage;
    [SerializeField] private int _ammoMaxCount;
    [SerializeField] private Bubble[] _bubblePrefabs = new Bubble[4];

    private PlayerInfoManager _playerInfoManager;
    private InventoryManager _inventoryManager;

    private int _ammoType;
    private int _ammoCount;

    private bool _isInCooldown = false;
    private bool _canShoot = true;

    private void Start()
    {
        _inventoryManager = SL.Get<InventoryManager>();
        _playerInfoManager = SL.Get<PlayerInfoManager>();
        _ammoCount = _ammoMaxCount;
        _playerInfoManager.UpdateAmmo((float)_ammoCount/(float)_ammoMaxCount);
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

            if (index != 2)
            {
                _playerAnimator.SetTrigger("Reload");

                DOVirtual.DelayedCall(_delayReloadMid, () =>
                {
                    _bottles.GetChild(_ammoType).gameObject.SetActive(false);
                    _ammoType = index;
                    _bottles.GetChild(_ammoType).gameObject.SetActive(true);
                });

                DOVirtual.DelayedCall(_delayReloadFull, () =>
                {
                    _ammoCount = _ammoMaxCount;
                    _playerInfoManager.UpdateAmmo((float)_ammoCount / (float)_ammoMaxCount);
                });
            }
            else
            {
                _playerAnimator.SetTrigger("Heal");

                _canShoot = false;

                DOVirtual.DelayedCall(_delayHealingMid, () =>
                {
                    _bottles.GetChild(_ammoType).gameObject.SetActive(false);
                    _bottles.GetChild(2).gameObject.SetActive(true);
                });

                DOVirtual.DelayedCall(_delayHealingMidd, () =>
                {
                    _playerHealthContoller.GetHeal(200);
                    _canShoot = true;
                });

                DOVirtual.DelayedCall(_delayHealingFull, () =>
                {
                    _bottles.GetChild(2).gameObject.SetActive(false);
                    _bottles.GetChild(_ammoType).gameObject.SetActive(true);
                });
            }
        }
    }

    public void Shoot()
    {
        if (!_isInCooldown && _ammoCount > 0 && _canShoot)
        {
            _ammoCount = Mathf.Clamp(--_ammoCount, 0, _ammoMaxCount);
            _playerInfoManager.UpdateAmmo((float)_ammoCount/(float)_ammoMaxCount);
            StartCooldown();
            Vector3 start = GetRandomPositionInCircle(_shootingStartPoint.position, _startOffsetRadius, transform.forward);
            Vector3 end = GetRandomPositionInCircle(_shootingEndPoint.position, _endOffsetRadius, transform.forward);

            Bubble bubble = Instantiate(_bubblePrefabs[_ammoType], start, Quaternion.identity, SL.Get<GarbageManager>().garbageParent);
            bubble.Init((end - start).normalized, _damage);
        }
    }

    private void StartCooldown()
    {
        _isInCooldown = true;
        float cooldownTime = 1 / _bubblesPerSecond;
        DOVirtual.DelayedCall(cooldownTime, () => { _isInCooldown = false; });
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
