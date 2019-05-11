﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject nucleo;
    public float speed;
    private void Awake()
    {
        nucleo = GameObject.FindGameObjectWithTag("Nucleo");
    }
    void Update()
    {
        transform.LookAt(nucleo.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
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
