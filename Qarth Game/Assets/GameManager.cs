using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class GameManager : MonoBehaviour
{
    public Bawss bossPrefab;
    public Transform[] spawnerEnemys;
    public float timerSpawn;
    public float secondsToWin;
    public GameObject screenWin, screenDefeat, particlesWin;
    float _timer;
    public AudioSource music;
    bool stopSpawn, doubleSpawn;
    bool part1, part2, part3, part4, part5, part6;
    float timeElapsed;
    public PostProcessingProfile profile;
    public List<float> hueChange;

    bool spawnearSuccionadores;

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawn)
        {
            _timer += Time.deltaTime;
            if (_timer >= timerSpawn)
            {
                RandomSpawner();
                _timer = 0;
            }
        }
        CheckPartsMusic();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(music.time);
            Win();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(music.time);
            Lose();
        }

        // Check Win
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= secondsToWin) Win();

        //Check Lose
        if (Nucleo.Instance.life < 1) Lose();

        if(Input.GetKeyDown(KeyCode.P))
        {
            music.time = 108.4f;
            stopSpawn = true;
        }

        UIManager.Instance.ChangeProgress(music.time);
    }

    void Win()
    {
        screenWin.gameObject.SetActive(true);
        particlesWin.gameObject.SetActive(true);
       // Time.timeScale = 0;
    }

    void Lose()
    {
        screenDefeat.gameObject.SetActive(true);
       // Time.timeScale = 0;
    }

    void RandomSpawner()
    {
        Enemy b = EnemySpawner.Instance.pool.GetObject();
        if (spawnearSuccionadores && Random.value <= .2f)
            b = EnemySpawner.Instance.poolEnemySucker.GetObject();

        int random = Random.Range(0, spawnerEnemys.Length - 1);
        int randomPX = Random.Range(-20, 20);
        b.transform.position = spawnerEnemys[random].position;
        FeedbackBorders.Instance.StartCoroutine(FeedbackBorders.Instance.ActivateBorder(random));
        b.transform.position += new Vector3 (randomPX, 0);
    }
    
    public void ChangePostProcess(int step)
    {
        ColorGradingModel.Settings algo = profile.colorGrading.settings;
        algo.basic.hueShift = hueChange[step];
    }
    
    void CheckPartsMusic()
    {
        if (!part1)
        {
            if (music.time >= 0 && music.time <= 1)
            {
                timerSpawn = 3f;
                part1 = true;
                Debug.Log("PARTE 1");
            }
        }
        /*if (!part2)
        {
            if (music.time >= 34.7f && music.time <= 34.9f)
            {
                timerSpawn = 3.5f;
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
        }*/
        if (!part2)
        {
            if (music.time >= 58.8f && music.time <= 58.99f)
            {
                timerSpawn = 2.5f;
                part2 = true;
                Debug.Log("PARTE 2");
            }
        }
        if (!part3)
        {
            if (music.time >= 82.5f && music.time <= 82.7f)
            {
                spawnearSuccionadores = true;
                Debug.Log("PARTE 3");
                part3 = true;
            }
        }
        if (!part4)
        {
            if (music.time >= 109.6f && music.time <= 109.8f)
            {
                timerSpawn = 5f;
                Instantiate(bossPrefab).transform.position = new Vector3(30, 1, 14);
                part4 = true;
                Debug.Log("PARTE 4: TRANQUILA");
            }
        }
        if (!part5)
        {
            if (music.time >= 128.6f && music.time <= 128.8f)
            {
                stopSpawn = true;
                Instantiate(bossPrefab).transform.position = new Vector3(30, 1, 14);
                part5 = true;
                Debug.Log("PARTE 5: BOSS");
            }
        }
    }
}
