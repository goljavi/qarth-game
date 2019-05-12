using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bawss : Enemy
{
    public int life;
    float _size;
    Vector3 _posSucker;
    public ParticleSystem normalParticles, particlesSucker;
    bool _suckerActive;
    Wall _wall;
    public float timeDamage;
    float _timer;
    float speed2 = 0.5f;

    public AudioSource dieAudiosource;

    protected override void Awake()
    {
        base.Awake();
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
                    speed = speed2;
                }
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
        if (other.gameObject.layer == 9 && !_suckerActive)
        {
            _wall = other.gameObject.GetComponent<Wall>();
            _wall.Disconnect();
            _suckerActive = true;
            _posSucker = transform.position;
            normalParticles.Stop();
            particlesSucker.Play();
        }
        else if (other.gameObject.layer == 10)
        {
            //ACA TOCA NUCLEO
            FindObjectOfType<GameManager>().Lose();
            TurnOff(this);
        }
    }

    public void TurnOff(Bawss e)
    {
        Destroy(gameObject);
    }
}
