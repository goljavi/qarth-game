using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI player1Amount;
    public TextMeshProUGUI player2Amount;

    private void Awake()
    {
        Instance = this;
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
