using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnerEnemys;
    public float timerSpawn;
    public float secondsToWin;

    float _timer;
    public AudioSource music;
    bool stopSpawn, doubleSpawn;
    bool part1, part2, part3, part4, part5, part6;
    float timeElapsed;


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
                if (doubleSpawn)
                {
                    RandomDoubleSpawner();
                }
                else
                {
                    RandomSpawner();
                }

                _timer = 0;

            }

        }
        CheckPartsMusic();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(music.time);
        }

        // Check Win
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= secondsToWin) Win();

        //Check Lose
        if (Nucleo.Instance.life < 1) Lose();
    }

    void Win()
    {
        Debug.Log("Win");
        
    }

    void Lose()
    {
        Debug.Log("Lose");
    }

    void RandomSpawner()
    {
        var b = EnemySpawner.Instance.poolEnemySucker.GetObject();
        int random = Random.Range(0, spawnerEnemys.Length - 1);
        int randomPX = Random.Range(-1, 1);
        b.transform.position = spawnerEnemys[random].position;
        b.transform.position += new Vector3 (randomPX, 0);
    }
    void RandomDoubleSpawner()
    {
        var a = EnemySpawner.Instance.pool.GetObject();
        var b = EnemySpawner.Instance.pool.GetObject();
        int random = Random.Range(0, spawnerEnemys.Length - 1);
        int random2;

        if(random == spawnerEnemys.Length)
        {
            random2 = random - 1;
        }else
        {
            random2 = random + 1;
        }

        int randomPX = Random.Range(-1, 1);
        a.transform.position = spawnerEnemys[random].position;
        b.transform.position = spawnerEnemys[random2].position;
        a.transform.position += new Vector3(randomPX, 0);
        b.transform.position += new Vector3(randomPX, 0);
    }
    void CheckPartsMusic()
    {
        if (!part1)
        {
            if (music.time >= 22.8f && music.time <= 22.99f)
            {
                timerSpawn = 2.5f;
                part1 = true;
                Debug.Log("PARTE 1");
            }
        }
        if (!part2)
        {
            if (music.time >= 34.7f && music.time <= 34.9f)
            {
                timerSpawn = 2f;
                part2 = true;
                Debug.Log("PARTE 2");
            }
        }
        if (!part3)
        {
            if (music.time >= 46.7f && music.time <= 46.9f)
            {
                doubleSpawn = true;
                part3 = true;
                Debug.Log("PARTE 3");
            }
        }
        if (!part4)
        {
            if (music.time >= 58.8f && music.time <= 58.99f)
            {
                timerSpawn = 1.8f;
                part4 = true;
                Debug.Log("PARTE 4");
            }
        }
        if (!part5)
        {
            if (music.time >= 82.5f && music.time <= 82.7f)
                timerSpawn = 2f;
            else if (music.time >= 95.0f && music.time <= 95.2f)
            {
                Debug.Log("PARTE 5");
                timerSpawn = 1.5f;
                part5 = true;
            }
        }
        if (!part6)
        {
            if (music.time >= 108.3f && music.time <= 108.5f)
                stopSpawn = true;
            else if (music.time >= 128.6f && music.time <= 128.8f)
            {
                stopSpawn = false;
                //ACA APARECE BOSS
                part6 = true;
                Debug.Log("PARTE 6: BOSS");
            }
        }
    }
}
