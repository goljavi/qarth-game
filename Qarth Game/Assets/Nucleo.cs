using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Nucleo : MonoBehaviour
{
    public int life;
    public TextMeshProUGUI lifeHUD;
    public static Nucleo Instance;
    float _timer, _timer2;
    public Animator anim;
    GameManager manager;
    private void Start()
    {
        Instance = this;
        manager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        _timer += Time.deltaTime / 2;
        _timer2 += Time.deltaTime / 3;
        //transform.Rotate(_timer, _timer2, transform.rotation.z);
        transform.rotation = new Quaternion(_timer, _timer2, transform.rotation.z, _timer);
    }
    public void AddVelocity(int numberVelocity)
    {
        if (manager.finishLevel)
            return;

        anim.speed = numberVelocity;
    }

    public void DeathNucleo()
    {
        anim.speed = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Damage();
        }

    }

    public void Damage()
    {
        life--;
        lifeHUD.text = "" + life;
        anim.SetTrigger("Damage");
    }
    public void AddLife()
    {
        life++;
        lifeHUD.text = "" + life;
    }
}
