using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemiesManager : MonoBehaviour, IService
{
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField, ReadOnly] private List<EnemyController> _enemies;
    
    private PlayerInfoManager _playerInfoManager;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        RegisterService();
    }

    private void OnDestroy()
    {
        UnregisterService();
    }

    private void Start()
    {
        _playerInfoManager = SL.Get<PlayerInfoManager>();
    }

    [ContextMenu("TestEnemy")]
    public void TestEnemy()
    {
        EnemyInfo info = new EnemyInfo();
        info.moveSpeed = 3;
        info.health = 3;
        info.position = Vector3.up * 0.1f;
        
        CreateEnemy(info);
    }
    
    public void CreateEnemy(EnemyInfo info)
    {
        EnemyController newEnemy = Instantiate(_enemyPrefab, info.position, Quaternion.identity);
        newEnemy.Init(info, _playerTransform);
        _enemies.Add(newEnemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }

    public List<EnemyController> GetEnemies() => _enemies;
    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }
}
