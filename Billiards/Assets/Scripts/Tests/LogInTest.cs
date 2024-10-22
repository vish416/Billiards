using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LogInTest
{
    private PlayfabManager _playfabManager;
    private GameObject _gameObject;

    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject();
        _playfabManager = _gameObject.AddComponent<PlayfabManager>();
        _playfabManager.messageText = new GameObject().AddComponent<Text>();
        _playfabManager.emailInput = new GameObject().AddComponent<InputField>();
        _playfabManager.passwordInput = new GameObject().AddComponent<InputField>();
        _playfabManager.emailInput.text = "test@test.com";
        _playfabManager.passwordInput.text = "123456";
    }
    
    // The test will try and log in with correct email and password, and will check to see if successful
    [Test]
    public void Login_ValidCredentials_Should_LogInSuccessfully()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _playfabManager.emailInput.text,
            Password = _playfabManager.passwordInput.text
        };

        _playfabManager.LoginButton();
        var result = new LoginResult();
        _playfabManager.OnLoginSuccess(result);

        Assert.AreEqual("Logged in", _playfabManager.messageText.text);
    }

    // The test will try and log in with incorrect email and password, and will check to see if unsuccessful
    [Test]
    public void Login_InvalidPassword_Should_NotLogIn()
    {

        _playfabManager.passwordInput.text = "wrongpassword"; 

        var request = new LoginWithEmailAddressRequest
        {
            Email = _playfabManager.emailInput.text,
            Password = _playfabManager.passwordInput.text
        };

        _playfabManager.LoginButton();
        var error = new PlayFabError
        {
            ErrorMessage = "Invalid email or password.",
        };
        _playfabManager.OnError(error);

        Assert.AreEqual("Invalid email or password.", _playfabManager.messageText.text);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(_gameObject);
    }
}
