using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class HomeScreenManager : MonoBehaviour
{
    public GameObject loginButton;  
    public GameObject accountButton;  

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
        }
        else
        {
            accountButton.SetActive(false);
            loginButton.SetActive(true);
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
}

