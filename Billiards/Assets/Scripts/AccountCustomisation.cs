using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class AccountCustomisation : MonoBehaviour
{
    
    public Text NameText;
    public Button EditButton;
    public InputField NameField;
    public Button SubmitButton;
    
	
    // Start is called before the first frame update
    void Start()
    {
        
        NameText.gameObject.SetActive(true);
        EditButton.gameObject.SetActive(true);
        NameField.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        
        GetDisplayName();
    }

    public void OnEditButtonClicked()
    {
        
        NameText.gameObject.SetActive(false);
        EditButton.gameObject.SetActive(false);
        NameField.gameObject.SetActive(true);
        SubmitButton.gameObject.SetActive(true);
    }
    
    public void OnSubmitButtonClicked()
    {
        
        string newName = NameField.text;

        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = newName
        };
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnError);
    }
    
    void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {

        NameText.text = result.DisplayName;
        
        NameField.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        NameText.gameObject.SetActive(true);
        EditButton.gameObject.SetActive(true);
    }
    
    void GetDisplayName()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), (result) => {
            string currentDisplayName = result.AccountInfo.TitleInfo.DisplayName;
            NameText.text = currentDisplayName;
        }, OnError);
    }
    
    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }

    public void LogoutButtonClicked() 
    {
	PlayFabClientAPI.ForgetAllCredentials();
	SceneManager.LoadScene("HomeScreen");
    
    }
}
