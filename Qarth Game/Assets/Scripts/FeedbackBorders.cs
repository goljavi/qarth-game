using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBorders : MonoBehaviour
{
    public static FeedbackBorders Instance;
    private AudioSource audiosrc;
    public List<GameObject> borders = new List<GameObject>();

    private void Awake()
    {
        audiosrc = GetComponent<AudioSource>();
        Instance = this;
        //StartCoroutine(ActivateBorder(1));
    }

    public IEnumerator ActivateBorder(int ID)
    {
        borders[ID].gameObject.GetComponent<Renderer>().material.SetFloat("_BaseLerp", 1f);
        yield return new WaitForSeconds(3.5f);
        audiosrc.Play();
        DeactivateBorder(ID);
    }

    public void DeactivateBorder(int ID)
    {
        borders[ID].gameObject.GetComponent<Renderer>().material.SetFloat("_BaseLerp", 0f);
    }
}
