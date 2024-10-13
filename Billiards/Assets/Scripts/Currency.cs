using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public const string Coins = "Coins";
    public static int coins = 600;
    // Start is called before the first frame update
    void Start()
    {
       coins = PlayerPrefs.GetInt("Coins");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateCoins(){
        PlayerPrefs.SetInt("Coins", coins);
        coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.Save();
    }
}
