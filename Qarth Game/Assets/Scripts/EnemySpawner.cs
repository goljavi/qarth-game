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
    }



    public Enemy EnemyFactory()
    {
        return Instantiate(enemyPrefab);
    }

    public Enemy enemyPrefab;

    public ObjectPool<Enemy> pool;

    public void ReturnEnemy(Enemy e)
    {
        pool.ReturnObject(e);
    }
}
