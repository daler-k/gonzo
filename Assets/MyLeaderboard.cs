using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class MyLeaderboard : MonoBehaviour
{
    #region Public_Vars
	public string leaderboard = "CgkIyND9rawGEAIQBw";
    #endregion
    #region Ui_Callbacks
    
    ///
    void Start()
    {

     //   PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

     //// enables saving game progress.
     //.EnableSavedGames()
     //       // registers a callback to handle game invitations received while the game is not running.

     //.Build();

     //   PlayGamesPlatform.InitializeInstance(config);
     //   // recommended for debugging:
     //   PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        LogIn();
    }

    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
                OnShowLeaderBoard();
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(SaveManager.bestDistance, leaderboard, (bool success) =>
            {
                if (success)
                {
                    ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
                }
                else
                {
                    Debug.Log("Add Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion
}