using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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

    [SerializeField] private Transform _firstWagon;
    [SerializeField] private Transform _secondWagon;

    [SerializeField] private List<Wave> _waves;

    [SerializeField] private float _playerSpeed;

    private TrainController _trainController;
    
    IEnumerator RunWaves()
    {
        foreach (Wave wave in _waves)
        {
            foreach (Vector2Int subWave in wave.subWaves)
            {
                bool trainArrived = false;
                _trainController.Arrive(() =>
                {
                    SpawnEnemies(subWave, wave);
                    trainArrived = true;
                });
                while (!trainArrived || _enemyManager.GetEnemies().Count > 0)
                {
                    yield return null;
                }
                
                _trainController.Leave();

                yield return new WaitForSeconds(subWave.y);
            }
            SL.Get<CurrencyManager>().AddCurrency(wave.waveReward);
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnEnemies(Vector2Int subWave, Wave wave)
    {
        

        for (int i = 0; i < subWave.x; i++)
        {
            int prob = Random.Range(0, 100);

            Vector3 cubeCenter = prob > 50 ? _firstWagon.position : _secondWagon.position;
            Vector3 cubeScale = prob > 50 ? _firstWagon.localScale : _secondWagon.localScale;;

            cubeScale /= 2;
            
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
        _trainController = SL.Get<TrainController>();
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
