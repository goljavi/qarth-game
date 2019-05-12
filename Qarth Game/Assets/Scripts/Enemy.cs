using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject nucleo;
    public float speed;
    public AudioSource chocarAudiosrc;
    private void Awake()
    {
        nucleo = GameObject.FindGameObjectWithTag("Nucleo");
    }
    void Update()
    {
        transform.LookAt(nucleo.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if(other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Wall>().Hit();
            chocarAudiosrc.Play();
            TurnOff(this);
        }
        else if (other.gameObject.layer == 10)
        {
            //ACA TOCA NUCLEO
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
