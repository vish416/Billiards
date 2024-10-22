using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class statisticsScript : MonoBehaviour
{


    public Text win;
    public Text loss;
    public Text winLoss;
    
    public int wins = 0;
    public int losses = 0;
  
    // Start is called before the first frame update
    void Start()
    {
	if (PlayFabClientAPI.IsClientLoggedIn())
	{
            loadStatistics();
	}
	else
	{
	    Debug.Log("Can't load statistics - not logged in");
	}
	updateStatisticsUI();
    }
	
    public void addWin() 
    {
        Currency.coins += 50;
        Currency.updateCoins();
	wins++;
	saveStatistics();
	updateStatisticsUI();
    }

    public void addLoss() 
    {
	losses++;
	saveStatistics();
	updateStatisticsUI();
    }

    private void saveStatistics() 
    {
	var request = new UpdateUserDataRequest 
	{
            	Data = new Dictionary<string,string>
		{
			{"Wins", wins.ToString()},
			{"Losses", losses.ToString()}
		}
	};
	PlayFabClientAPI.UpdateUserData(request, OnDataSendSuccess, OnError);
    }

    private void loadStatistics()
    {
	PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    private void OnDataSendSuccess(UpdateUserDataResult result)
    {
	Debug.Log("Statistics saved");
    }

    private void OnDataReceived(GetUserDataResult result)
    {
	if (result.Data != null && result.Data.ContainsKey("Wins") && result.Data.ContainsKey("Losses"))
        {
            wins = int.Parse(result.Data["Wins"].Value);
            losses = int.Parse(result.Data["Losses"].Value);
        }
        updateStatisticsUI();
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }
 	
    private void updateStatisticsUI()
    {	
	if (win != null)
		win.text = "Win:         " + wins.ToString();
	if (loss != null)
       		loss.text = "Loss:        " + losses.ToString();
	if (winLoss != null)
	{
		float winLossRatio = losses == 0 ? wins : (float)wins/losses;
    		winLoss.text = "Win/Loss:    " + winLossRatio.ToString("F2");
	}
    }
}
