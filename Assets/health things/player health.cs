using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    public TextMeshProUGUI te;
    public int player1Health;
    public const int maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        player1Health = maxHealth;
        te.text = "live " + player1Health;
    }

    // Update is called once per frame
    public void player1takeAdamage(int amount)
    {
        player1Health -= amount;
        te.text = "live " + player1Health;
        if(player1Health <= 0)
        {
            Debug.Log("Game Over Player 1");
        }
    }

}
