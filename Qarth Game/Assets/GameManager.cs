using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Bawss bossPrefab;
    public Transform[] spawnerEnemys;
    public float timerSpawn;
    float secondsToWin = 155;
    public GameObject screenWin, screenDefeat, particlesWin;
    float _timer;
    public AudioSource music;
    bool stopSpawn, doubleSpawn;
    bool part1, part2, part3, part4, part5, part6;
    float timeElapsed;
    public PostProcessingProfile profile;
    public CinemachinePostFX profileFX;
    public List<PostProcessingProfile> profiles = new List<PostProcessingProfile>();
    //public List<float> hueChange;

    bool spawnearSuccionadores;
    public bool finishLevel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && finishLevel)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.I) )
        {
            Nucleo.Instance.AddLife();
        }
        if (finishLevel)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePostProcess(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePostProcess(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePostProcess(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangePostProcess(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangePostProcess(4);
        }
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


        // Check Win
        timeElapsed += Time.deltaTime;
        Debug.Log(timeElapsed + " - " + secondsToWin);
        if (timeElapsed >= secondsToWin) Win();

        //Check Lose
        if (Nucleo.Instance.life < 1) Lose();

        if(Input.GetKeyDown(KeyCode.P))
        {
            music.time = 108.4f;
            timeElapsed += 108.4f;
            stopSpawn = true;
        }

        UIManager.Instance.ChangeProgress(music.time);
    }

    void Win()
    {
        finishLevel = true;
        screenWin.gameObject.SetActive(true);
        particlesWin.gameObject.SetActive(true);
        Destroy(FindObjectOfType<Bawss>().gameObject);
       // Time.timeScale = 0;
    }

    public void Lose()
    {
        finishLevel = true;
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
        Debug.Log(step);
        profileFX.m_Profile = profiles[step];
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

                ChangePostProcess(0);
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
                ChangePostProcess(1);
            }
        }
        if (!part3)
        {
            if (music.time >= 82.5f && music.time <= 82.7f)
            {
                spawnearSuccionadores = true;
                Debug.Log("PARTE 3");
                part3 = true;
                ChangePostProcess(2);
            }
        }
        if (!part4)
        {
            if (music.time >= 109.6f && music.time <= 109.8f)
            {
                timerSpawn = 5f;
                part4 = true;
                Debug.Log("PARTE 4: TRANQUILA");
                ChangePostProcess(3);
            }
        }
        if (!part5)
        {
            if (music.time >= 127.6f && music.time <= 127.8f)
            {
                Instantiate(bossPrefab).transform.position = new Vector3(40, 1, 25);
                part5 = true;
                Debug.Log("PARTE 5: BOSS");
                ChangePostProcess(4);
            }
        }
    }
}
