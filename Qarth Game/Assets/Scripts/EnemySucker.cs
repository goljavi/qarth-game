using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySucker : Enemy
{
    public int life;
    float _size;
    Vector3 _posSucker;
    public ParticleSystem normalParticles, particlesSucker;
    bool _suckerActive;
    Wall _wall;
    public float timeDamage;
    float _timer;

    public AudioSource absorbAudiosrc;
    public AudioSource dieAudiosource;

    private void Awake()
    {
        nucleo = GameObject.FindGameObjectWithTag("Nucleo");
        if(particlesSucker == null)
            particlesSucker = GetComponentInChildren<ParticleSystem>();
        if (normalParticles == null)
            normalParticles = GetComponent<ParticleSystem>();
        life = 1;
    }
    void Update()
    {
        if (life <= 0)
        {
            dieAudiosource.Play();
            TurnOff(this);
        }
        transform.LookAt(nucleo.transform.position);
        if (!_suckerActive)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            if (!normalParticles.isPlaying)
            {
                normalParticles.Play();
                particlesSucker.Stop();
            }
        }
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
                        {
                            life--;
                            _size -= 0.5f;
                        }
                        else
                        {
                            life++;
                            _size += 0.5f;
                        }

                        _suckerActive = false;
                        _wall = null;
                        normalParticles.Play();
                        particlesSucker.Stop();
                    }
                    else
                    {
                        _wall.Hit();
                        _timer = 0;


                        if (_wall.violetWall)
                        {
                            life--;
                            _size -= 0.5f;
                        }
                        else
                        {
                            life++;
                            _size += 0.5f;
                        }
                    }
                }
                _size = Mathf.Clamp(_size, 1, 3);
                transform.localScale = new Vector3(_size, _size, _size);
            }
            else
            {
                _timer = 0;
                _suckerActive = false;
                _wall = null;
                normalParticles.Play();
                particlesSucker.Stop();
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
            normalParticles.Stop();
            particlesSucker.Play();
            absorbAudiosrc.Play();
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
        e._size = 1;
        e.gameObject.SetActive(true);

        if(e.normalParticles != null)
            e.normalParticles.Play();
        e.particlesSucker.Stop();
    }

    public static void TurnOff(EnemySucker e)
    {
        e.life = 1;
        e.gameObject.SetActive(false);
    }
}
