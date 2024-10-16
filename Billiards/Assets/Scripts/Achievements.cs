using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public const string FirstWin = "FirstWin";
    public const string FirstPurchase = "FirstPurchase";
    public const string First1000 = "First1000";
    public const string First3Skins = "First3Skins";
    public static int firstWinAchievement = 0;
    public static int firstPurchaseAchievement = 0;
    public static int first1000Achievement = 0;
    public static int first3SkinsAchievement = 0;
    private Purchase purchaseScript;
    // Start is called before the first frame update
    void Start()
    {
        firstWinAchievement = PlayerPrefs.GetInt("FirstWin");
        UpdateFirstWinAchievement();
        firstPurchaseAchievement = PlayerPrefs.GetInt("FirstPurchase");
        UpdateFirstPurchaseAchievement();
        first1000Achievement = PlayerPrefs.GetInt("First1000");
        UpdateFirst1000Achievement();
        first3SkinsAchievement = PlayerPrefs.GetInt("First3Skins");
        UpdateFirst3SkinsAchievement();
        purchaseScript = GetComponentInParent<Purchase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateFirstWinAchievement(){
        PlayerPrefs.SetInt("FirstWin", firstWinAchievement);
        firstWinAchievement = PlayerPrefs.GetInt("FirstWin");
        PlayerPrefs.Save();
    }

    public void FirstWinAchieved(){
        if(statisticsScript.wins == 1){
            Currency.coins += 100;
            firstWinAchievement = 1;
            UpdateFirstWinAchievement();
        }
    }

    public static void UpdateFirstPurchaseAchievement(){
        PlayerPrefs.SetInt("FirstPurchase", firstPurchaseAchievement);
        firstPurchaseAchievement = PlayerPrefs.GetInt("FirstPurchase");
        PlayerPrefs.Save();
    }

    public void FirstPurchaseAchieved(){
        if(Purchase.tablePurchase == 1 && Purchase.cuePurchase == 0 && Purchase.eyePurchase == 0){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }

        if(Purchase.tablePurchase == 0 && Purchase.cuePurchase == 1 && Purchase.eyePurchase == 0){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }

        if(Purchase.tablePurchase == 0 && Purchase.cuePurchase == 0 && Purchase.eyePurchase == 1){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }
    }

    public static void UpdateFirst1000Achievement(){
        PlayerPrefs.SetInt("FirstWin", firstWinAchievement);
        firstWinAchievement = PlayerPrefs.GetInt("FirstWin");
        PlayerPrefs.Save();
    }

    public void First1000Achieved(){
        if(Currency.coins <= 1000){
            Currency.coins += 100;
            firstWinAchievement = 1;
            UpdateFirst1000Achievement();
        }
    }

    public static void UpdateFirst3SkinsAchievement(){
        PlayerPrefs.SetInt("First3Skins", first3SkinsAchievement);
        first3SkinsAchievement = PlayerPrefs.GetInt("First3Skins");
        PlayerPrefs.Save();
    }

    public void First3SkinsAchieved(){
        if(Purchase.tablePurchase == 1 && Purchase.cuePurchase == 1 && Purchase.eyePurchase == 1){
            Currency.coins += 100;
            first3SkinsAchievement = 1;
            UpdateFirst3SkinsAchievement();
        }
    }
}
