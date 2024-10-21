using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class HomeScreenManager : MonoBehaviour
{
    public GameObject loginButton;  
    public GameObject accountButton;  
    public Text LoggedInText;

    void Start()
    {
        CheckLoginStatus();
    }

    void CheckLoginStatus()
    {
    
        if (PlayFabClientAPI.IsClientLoggedIn())
        {   
            accountButton.SetActive(true);
            loginButton.SetActive(false);
	    GetDisplayName();
        }
        else
        {
            accountButton.SetActive(false);
            loginButton.SetActive(true);
            LoggedInText.gameObject.SetActive(false);
        }
    }

    public void OnLoginButtonClicked()
    {
        SceneManager.LoadScene("SignUpScene"); 
    }

    public void OnAccountButtonClicked()
    {
        SceneManager.LoadScene("AccountCustomisationScene"); 
    }

    void GetDisplayName()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), (result) => {
            string currentDisplayName = result.AccountInfo.TitleInfo.DisplayName;
            LoggedInText.text = "Logged In: " + currentDisplayName;
        }, OnError);
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }
}

