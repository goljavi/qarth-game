using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static EnemySpawner _Instance;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<Enemy>(EnemyFactory, Enemy.TurnOn, Enemy.TurnOff, 15, true);
        poolEnemySucker = new ObjectPool<EnemySucker>(EnemySuckerFactory, EnemySucker.TurnOn, EnemySucker.TurnOff, 5, true);
    }

    public Enemy EnemyFactory()
    {
        return Instantiate(enemyPrefab);
    }
    public EnemySucker EnemySuckerFactory()
    {
        return Instantiate(enemySuckerPrefab);
    }

    public Enemy enemyPrefab;
    public EnemySucker enemySuckerPrefab;

    public ObjectPool<Enemy> pool;
    public ObjectPool<EnemySucker> poolEnemySucker;

    public void ReturnEnemy(Enemy e)
    {
        pool.ReturnObject(e);
    }
    public void ReturnEnemy(EnemySucker e)
    {
        pool.ReturnObject(e);
    }
}
