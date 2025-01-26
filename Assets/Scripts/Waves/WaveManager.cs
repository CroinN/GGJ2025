using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour, IService
{
    [Serializable] public struct Wave
    {
        public List<Vector2Int> subWaves;
        public int waveReward;
        public float xModifier;
        public float x12Modifier;
        public float x14Modifier;
    }

    [SerializeField] private EnemiesManager _enemyManager;

    [SerializeField] private Transform _spawningCube;

    [SerializeField] private List<Wave> _waves;

    [SerializeField] private float _playerSpeed;
    
    IEnumerator RunWaves()
    {
        foreach (var wave in _waves)
        {
            foreach (var subWave in wave.subWaves)
            {
                SpawnEnemies(subWave, wave);
                while (_enemyManager.GetEnemies().Count > 0)
                {
                    yield return null;
                }

                yield return subWave.y;
            }
        }
    }

    private void SpawnEnemies(Vector2Int subWave, Wave wave)
    {
        Vector3 cubeCenter = _spawningCube.position;
        Vector3 cubeScale = _spawningCube.localScale;

        cubeScale /= 2;

        for (int i = 0; i < subWave.x; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(cubeCenter.x - cubeScale.x, cubeCenter.x + cubeScale.x),
                cubeCenter.y,
                Random.Range(cubeCenter.z - cubeScale.z, cubeCenter.z + cubeScale.z)
            );
            EnemyInfo info = new EnemyInfo
            {
                position = position,
                damage = 15,
                health = 100,
                moveSpeed = GetSpeed(wave)
            };
            _enemyManager.CreateEnemy(info);
        }
    }

    private float GetSpeed(Wave wave)
    {
        float prob = Random.Range(0, 1);
        if (prob <= wave.xModifier) return _playerSpeed;
        if (prob > wave.xModifier && prob <= wave.x12Modifier) return _playerSpeed * 1.2f;
        return _playerSpeed * 1.4f;
    }

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private void Awake()
    {
        RegisterService();
    }

    private void OnDestroy()
    {
        UnregisterService();
    }

    public void RegisterService()
    {
        SL.Register(this);
    }

    public void UnregisterService()
    {
        SL.Unregister(this);
    }
}
