using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour {
    
    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton() 
    {
	if (passwordInput.text.Length < 6) {
	    messageText.text = "Password needs to be longer than 6 characters";
            return;
	}

        var request = new RegisterPlayFabUserRequest 
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) 
    {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton() 
    {
        var request = new LoginWithEmailAddressRequest {
		Email = emailInput.text,
		Password = passwordInput.text
	};
	PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void ResetPasswordButton() 
    {
        var request = new SendAccountRecoveryEmailRequest {
	Email = emailInput.text,
	TitleId = "402A7"
	};
	PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
	messageText.text = "Password reset email sent";
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization logic here
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic here
    }

	
    public void OnLoginSuccess(LoginResult result)
    {
	messageText.text = "Logged in";
	Debug.Log("Successful login");
    }

    public void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
	Debug.Log(error.GenerateErrorReport());
    }
}