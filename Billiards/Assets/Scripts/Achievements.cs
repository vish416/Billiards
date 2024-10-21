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
    public const string FirstLoss = "FirstLoss";
    public static int firstWinAchievement = 0;
    public static int firstPurchaseAchievement = 0;
    public static int first1000Achievement = 0;
    public static int first3SkinsAchievement = 0;
    public static int firstLossAchievement = 0;
    private Purchase purchaseScript;
    private statisticsScript statistics;
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
        first3SkinsAchievement = PlayerPrefs.GetInt("FirstLoss");
        UpdateFirstLossAchievement();
        purchaseScript = GetComponentInParent<Purchase>();
        first3SkinsAchievement = PlayerPrefs.GetInt("First3Skins", 0);
        UpdateFirst3SkinsAchievement();
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
        if(statistics.wins == 1){
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

    public static void FirstPurchaseAchieved(){
        if(Purchase.tablePurchase == 1 && Purchase.cuePurchase == 0 && Purchase.eyePurchase == 0){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }
        else if(Purchase.tablePurchase == 0 && Purchase.cuePurchase == 1 && Purchase.eyePurchase == 0){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }
        else if(Purchase.tablePurchase == 0 && Purchase.cuePurchase == 0 && Purchase.eyePurchase == 1){
            Currency.coins += 100;
            firstPurchaseAchievement = 1;
            UpdateFirstPurchaseAchievement();
        }
    }


    public static void UpdateFirst1000Achievement(){
        PlayerPrefs.SetInt("First1000", first1000Achievement);
        PlayerPrefs.Save();
        Debug.Log($"Achievement unlocked! First3SkinsAchievement: {first1000Achievement}");
    }

    public static void First1000Achieved()
    {       
        Currency.coins += 100;  // Award coins only once
        first1000Achievement = 1;
        UpdateFirst1000Achievement();
        Debug.Log("First1000 Achievement Unlocked!");
    }


    public static void UpdateFirst3SkinsAchievement(){
        PlayerPrefs.SetInt("First3Skins", first3SkinsAchievement);
        PlayerPrefs.Save();
    }

    public static void First3SkinsAchieved(){
        if(Purchase.tablePurchase == 1 && Purchase.cuePurchase == 1 && Purchase.eyePurchase == 1){
            Currency.coins += 100;
            first3SkinsAchievement = 1;
            UpdateFirst3SkinsAchievement();
        }
    }

    public static void UpdateFirstLossAchievement(){
        PlayerPrefs.SetInt("FirstLoss", firstLossAchievement);
        firstLossAchievement = PlayerPrefs.GetInt("FirstLoss");
        PlayerPrefs.Save();
    }

    public void FirstLossAchieved(){
        if(statistics.losses == 1){
            Currency.coins += 100;
            firstLossAchievement = 1;
            UpdateFirstLossAchievement();
        }
    }
}
