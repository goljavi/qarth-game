using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColor : MonoBehaviour
{
    public static MusicColor instance = null;
    AudioSource _music;
    float[] spectrum = new float[128];
    [SerializeField] [HideInInspector] float _minVolume = 0.005f;
    [SerializeField] public Color color = Color.white;
    public AudioClip song;

    float _red;
    float _green;
    float _blue;
    public float multiplyRed = 8.5f / 7;
    public float multiplyGreen = 1.7f / 5;
    public float multiplyBlue = 2.7f / 5;
    float _spectrumMultiply = 1;
    public float multiplySpectrum;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Destruye si ya hay una copia de este script
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _music = GetComponent<AudioSource>();
        _music.clip = song;
        _music.Play();
        //_music.loop = true;

    }


    void Update()
    {
        AnalyzeMusic();
        UpdateColor();
    }

    private void AnalyzeMusic()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = _minVolume;
        };

        _music.GetOutputData(spectrum, 0);
        _spectrumMultiply = multiplySpectrum / _music.volume;
    }

    private void UpdateColor()
    {
        _red = 0;
        for (int i = 1; i < 3; i++)
        {
            _red += spectrum[i];
        }
        _red *= multiplyRed * _spectrumMultiply;
        if (_red < 0) _red = 0;
        else if (_red > 1) _red = 1;

        _green = 0;
        for (int i = 5; i < 26; i++)
        {
            _green += spectrum[i];
        }
        _green *= multiplyGreen * _spectrumMultiply;
        if (_green < 0) _green = 0;
        else if (_green > 1) _green = 1;

        _blue = 0;
        for (int i = 36; i < 128; i++)
        {
            _blue += spectrum[i];
        }
        _blue *= multiplyBlue * _spectrumMultiply;
        if (_blue < 0) _blue = 0;
        else if (_blue > 1) _blue = 1;

        color = new Color(_red, _green, _blue);
    }

    public Color GetColor()
    {
        return color;
    }
}
