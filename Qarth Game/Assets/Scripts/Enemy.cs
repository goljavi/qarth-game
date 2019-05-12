using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject nucleo;
    public float speed;
    public AudioSource chocarAudiosrc;
    public ParticleSystem myparticles;
    GameManager manager;
    protected virtual void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        if (GetComponent<ParticleSystem>())
        {
            myparticles = GetComponent<ParticleSystem>();
        }
        else
        {
            myparticles = GetComponentInChildren<ParticleSystem>();
        }
        nucleo = GameObject.FindGameObjectWithTag("Nucleo");
    }
    void Update()
    {
        if (manager.finishLevel)
        {
            TurnOff(this);
        }
        myparticles.Play();
        transform.LookAt(nucleo.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Wall>().Hit();
            chocarAudiosrc.Play();
            TurnOff(this);
        }
        else if (other.gameObject.layer == 10)
        {
            chocarAudiosrc.Play();
            TurnOff(this);
        }
    }

    public static void TurnOn(Enemy e)
    {
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(Enemy e)
    {
        e.gameObject.SetActive(false);
    }

}
