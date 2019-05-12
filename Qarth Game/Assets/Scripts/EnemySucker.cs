using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySucker : Enemy
{
    public int life;
    Vector3 _posSucker;
    ParticleSystem _particlesSucker;
    bool _suckerActive;
    Wall _wall;
    public float timeDamage;
    float _timer;
    private void Awake()
    {
        nucleo = GameObject.FindGameObjectWithTag("Nucleo");
        _particlesSucker = GetComponentInChildren<ParticleSystem>();
        life = 1;
    }
    void Update()
    {
        if (life <= 0)
            TurnOff(this);
        transform.LookAt(nucleo.transform.position);
        if(!_suckerActive)
            transform.position += transform.forward * speed * Time.deltaTime;
        else
        {
            transform.position = _posSucker;
            if(_wall != null)
            {
                _timer += Time.deltaTime;
                if(_timer >= timeDamage)
                {
                    if(_wall.life <= 1)
                    {
                        _wall.Hit();
                        _timer = 0;

                        if (_wall.violetWall)
                            life--;
                        else
                            life++;

                        _suckerActive = false;
                        _wall = null;
                    }
                    else
                    {
                        _wall.Hit();
                        _timer = 0;

                        if (_wall.violetWall)
                            life--;
                        else
                            life++;
                    }
                }
                transform.localScale = new Vector3(life, life, life);
            }
            else
            {
                _timer = 0;
                _suckerActive = false;
                _wall = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 9 && !_suckerActive)
        {
            _wall = other.gameObject.GetComponent<Wall>();
            _suckerActive = true;
            _posSucker = transform.position;
            _particlesSucker.Play();
        }
        else if (other.gameObject.layer == 10)
        {
            //ACA TOCA NUCLEO
            TurnOff(this);
        }
    }


    public static void TurnOn(EnemySucker e)
    {
        e.gameObject.transform.localScale = Vector3.one;
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(EnemySucker e)
    {
        e.life = 1;
        e.gameObject.SetActive(false);
    }
}
