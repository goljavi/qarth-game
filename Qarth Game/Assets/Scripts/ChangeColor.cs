using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor;
    public Color color;
    MusicColor analyzer;
    public Renderer rend;
    public float substractColor;
    void Start()
    {
        analyzer = MusicColor.instance;
        rend = GetComponent<Renderer>();
    }
    private void Update()
    {
        newColor = analyzer.GetColor();

        //RED
        if (newColor.r > 1)
        {
            newColor.r = 1;
        }
        else if (newColor.r < 0)
        {
            newColor.r = 0;
        }

        //GREEN
        if (newColor.g > 1)
        {
            newColor.g = 1;
        }
        else if (newColor.g < 0)
        {
            newColor.g = 0;
        }

        //BLUE
        if (newColor.b > 1)
        {
            newColor.b = 1;
        }
        else if (newColor.b < 0)
        {
            newColor.b = 0;
        }
        /*  if (newColor.g >= 0.114)
          {
              if ( newColor.r < 0.9 && newColor.r < 0.9)
              {
                  newColor.g = 0.114f;
              }

          }*/
        /*    if (newColor.g > 0)
          {
              newColor.b = 1;
          } else
          {
              newColor.b = 0;
          }*/
        //newColor.b = 1;

        color = new Color(newColor.r - substractColor, newColor.g - substractColor, newColor.b - substractColor);

        rend.material.color = color;
        //RenderSettings.skybox.SetColor("_Tint", color);
    }

}