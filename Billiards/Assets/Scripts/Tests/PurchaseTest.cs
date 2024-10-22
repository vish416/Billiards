using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PurchaseTest
{
    private GameObject _gameObject;
    private Purchase _purchase;

    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject();
        _purchase = _gameObject.AddComponent<Purchase>();

        // Reset PlayerPrefs for a clean environment
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("TablePurchase", 0);
        PlayerPrefs.SetInt("CuePurchase", 0);
        PlayerPrefs.SetInt("EyePurchase", 0);

        // Reset static variables
        Purchase.tablePurchase = 0;
        Purchase.cuePurchase = 0;
        Purchase.eyePurchase = 0;

        // Set initial currency
        Currency.coins = 500;

        // Manually call Start() to initialize PlayerPrefs values
        _purchase.InitializePurchases();
    }

    [Test]
    public void TablePress_ShouldDeductCoinsAndUpdatePurchase()
    {
        _purchase.TablePress();

        Assert.AreEqual(100, Currency.coins, "Coins should be deducted after table purchase.");
        Assert.AreEqual(1, PlayerPrefs.GetInt("TablePurchase"), "Table purchase should be saved.");
    }
    
    [Test]
    public void CuePress_ShouldDeductCoinsAndUpdatePurchase()
    {
        _purchase.CuePress();

        Assert.AreEqual(100, Currency.coins, "Coins should be deducted after cue purchase.");
        Assert.AreEqual(1, PlayerPrefs.GetInt("CuePurchase"), "Cue purchase should be saved.");
    }

    [Test]
    public void BallPress_ShouldDeductCoinsAndUpdatePurchase()
    {
        _purchase.BallPress();

        Assert.AreEqual(100, Currency.coins, "Coins should be deducted after ball purchase.");
        Assert.AreEqual(1, PlayerPrefs.GetInt("EyePurchase"), "Ball purchase should be saved.");
    }

    [Test]
    public void TablePress_ShouldNotPurchase_IfNotEnoughCoins()
    {
        Currency.coins = 400; // Less than the required 500 coins

        _purchase.TablePress();

        Assert.AreEqual(400, Currency.coins, "Coins should not be deducted if insufficient.");
        Assert.AreEqual(0, PlayerPrefs.GetInt("TablePurchase"), "Table purchase should not be saved.");
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(_gameObject);
        PlayerPrefs.DeleteAll();  // Cleanup PlayerPrefs after each test
    }
}
