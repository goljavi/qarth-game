using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnerEnemys;
    public float timerSpawn;
    float _timer;
    public AudioSource music;
    bool stopSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawn)
        {
            _timer += Time.deltaTime;
            if (_timer >= timerSpawn)
            {
                _timer = 0;
                RandomSpawner();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(music.time);
            }
        }
        if (music.time >= 108.3f && music.time <= 108.5f)
            stopSpawn = true;
        else if (music.time >= 128.6f && music.time <= 128.8f)
        {
            stopSpawn = false;
            timerSpawn = 1.5f;
        }
    }
    void RandomSpawner()
    {
        var b = EnemySpawner.Instance.pool.GetObject();
        int random = Random.RandomRange(0, spawnerEnemys.Length);
        b.transform.position = spawnerEnemys[random].position;
    }
}
