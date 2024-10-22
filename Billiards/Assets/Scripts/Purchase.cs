using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    public const string TablePurchased = "TablePurchase";
    public const string CuePurchased = "CuePurchase";
    public const string EyePurchased = "EyePurchase";
    public static int tablePurchase = 0;
    public static int cuePurchase = 0;
    public static int eyePurchase = 0;
    // Start is called before the first frame update
    void Start()
    {
        tablePurchase = PlayerPrefs.GetInt("TablePurchase");
        cuePurchase = PlayerPrefs.GetInt("CuePurchase");
        eyePurchase = PlayerPrefs.GetInt("EyePurchase");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateTablePurchase(){
        PlayerPrefs.SetInt("TablePurchase", tablePurchase);
        tablePurchase = PlayerPrefs.GetInt("TablePurchase");
        PlayerPrefs.Save();
        Achievements.FirstPurchaseAchieved();
        Achievements.First3SkinsAchieved();
    }

    public static void UpdateCuePurchase(){
        PlayerPrefs.SetInt("CuePurchase", cuePurchase);
        cuePurchase = PlayerPrefs.GetInt("CuePurchase");
        PlayerPrefs.Save();
        Achievements.FirstPurchaseAchieved();
        Achievements.First3SkinsAchieved();
    }

    public static void UpdateEyePurchase(){
        PlayerPrefs.SetInt("EyePurchase", eyePurchase);
        eyePurchase = PlayerPrefs.GetInt("EyePurchase");
        PlayerPrefs.Save();
        Achievements.FirstPurchaseAchieved();
        Achievements.First3SkinsAchieved();
    }

    public void TablePress(){
        if(Currency.coins >= 500 && tablePurchase == 0){
            Currency.coins -= 500;
            tablePurchase = 1;
            UpdateTablePurchase();
        }
    }

    public void BallPress(){
        if(Currency.coins >= 500 && eyePurchase == 0){
            Currency.coins -= 500;
            eyePurchase = 1;
            UpdateEyePurchase();
        }
    }

    public void CuePress(){
        if(Currency.coins >= 500 && cuePurchase == 0){
            Currency.coins -= 500;
            cuePurchase = 1;
            UpdateCuePurchase();
        }
    }
}
