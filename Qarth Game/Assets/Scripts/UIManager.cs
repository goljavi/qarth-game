using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI player1Amount;
    public TextMeshProUGUI player2Amount;
    public Image progress; 

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeProgress(float seconds)
    {
        progress.fillAmount = seconds / 155;
    }

    public void ChangeUI(bool player1, int amount)
    {
        if (player1)
        {
            player1Amount.text = ( 4 -amount).ToString();
        }
        else
        {
            player2Amount.text = ( 4 -amount).ToString();
        }
    }


}
