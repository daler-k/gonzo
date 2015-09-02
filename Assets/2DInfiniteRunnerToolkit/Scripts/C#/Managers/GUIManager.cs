using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    public LevelManager levelManager;                       //A link to the level manager
    public MissionManager missionManager;                   //A link to the mission manager
    public PlayerManager playerManager;                     //A link to the player manager
    public PowerupManager powerupManager;                   //A link to the powerup manager

    public Text distanceText;                               //The main UI's distance text
    public Text coinText;                                   //The main UI's coin ammount text
    public Text healthText;                                 //The main UI's health text
    public Text speedAmmount;
    public Text bombAmmount;

    public Text finishDistanceText;                         //The finish menu's distance text
    public Text finishCoinText;                             //The finish menu's coin ammount text
    public Text pauseDistanceText;                         //The finish menu's distance text
    public Text pauseCoinText;                             //The finish menu's coin ammount text
    public Text currSpeedAmmText;
    public Text currBombAmmText;
    public Text currContAmmText;
    public Text bestDistanceText;
    public Text currContAmmTextCS;

    public GameObject mainUI;                               //The main UI

    public Sprite[] arrowSprites;                           //Up and down arrow sprites
    public Sprite[] audioSprites;                           //Audio enabled and disabled sprites
    public Sprite[] ShopSkinButtonSprites;                  //The buy, equip, equipped sprites

   // public Animator overlayAnimator;                        //The overlay animator
    public Animator topMenuAnimator;                        //The top menu animator
    public Animator shopAnimator;                           //The shop menu animator
    public Animator missionMenuAnimator;                    //The mission menu animator
    public Animator pauseMenuAnimator;                      //The pause menu animator
    public Animator finishMenuAnimator;                     //The finish menu animator
    public Animator[] powerupButtons;                       //The powerup buttons animator (extra speed, shield, sonic wave, revive)
    public Animator[] missionNotifications;                 //THe mission complete notification panels

    public RectTransform[] missionPanelElements;            //The mission panel elements (mission descriptions and mission status)

    public Text coinAmmount;                                //The shop menu coin ammount text
    public Text[] ShopOwnedItems;                           //The shop menu number of owned item texts

    public Image[] shopSubmarineButtons;                    //The shop menu submarine 1 and 2 buttons
    public Image[] audioButtons;                            //The audio buttons

    //Tells, which mission notification is used at the moment
    private bool[] usedMissionNotifications = new bool[]{false, false, false};

    private bool inPlayMode = false;

    private int collectedCoins = 0;
    private int distanceTraveled = 0;
    private int currHealth = 100;
    public LevelGenerator levelGenerator;

    public GameController gameController;

    public AudioManager audioManager;

    public const string AppId = "396436303886148";
    public const string ShareUrl = "http://www.facebook.com/dialog/feed";
    public string link;
    public string pictureLink;
    public string name;
    public string caption;
    public string description;
    public string redirectUri;

    public void ShareFacebook()
    {
        Application.OpenURL(ShareUrl + "app_id=" + AppId +
        "&link=" + WWW.EscapeURL(link) +
        "&picture=" + WWW.EscapeURL(pictureLink) +
        "&name=" + WWW.EscapeURL(name) +
        "&caption=" + WWW.EscapeURL(caption) +
        "&description=" + WWW.EscapeURL(description) +
        "&redirect_uri=" + WWW.EscapeURL(redirectUri));
    }
   

    //Called at the beginning of the game
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Updates the audio buttons sprites
        UpdateAudioButtons();
    }
    //Called at every frame
    void Update()
    {
        //If the game is in play mode
        if (inPlayMode)
        {
            //Debug.Log(SaveManager.coinAmmount);
            //Update the main UI's status texts
            coinText.text = AddDigitDisplay(collectedCoins, 4);
            distanceText.text = AddDigitDisplay(distanceTraveled, 5);
            healthText.text = AddDigitDisplay(currHealth, 3);
            bombAmmount.text = AddDigitDisplay(SaveManager.sonicWave, 1);
            speedAmmount.text = AddDigitDisplay(SaveManager.extraSpeed, 1);
        }
    }





    //Called, when the player clicks on the top menu arrow button
    public void ChangeTopMenuState(Image arrowImage)
    {
        //If the top menu is in the default position
        if (!topMenuAnimator.GetBool("MoveDown"))
        {
            //Change the button sprite, and move the menu down
            arrowImage.sprite = arrowSprites[1];
            topMenuAnimator.SetBool("MoveDown", true);
           // overlayAnimator.SetBool("Visible", true);
        }
        else
        {
            //If the top menu is visible, but but mission menu is not
            if (!missionMenuAnimator.GetBool("ShowMissions"))
            {
                //Hide the top menu
             //   overlayAnimator.SetBool("Visible", false);
                topMenuAnimator.SetBool("MoveDown", false);
                arrowImage.sprite = arrowSprites[0];
            }
            //If the top menu is visible, and the mission menu is visible as well
            else
            {
                //Hide the mission menu
                missionMenuAnimator.SetBool("ShowMissions", false);
            }
        }
    }
    //Called, when the player clicks on an audio button. Change audio state (enabled, disabled)
    public void ChangeAudioState()
    {
        //Change the state, and update the button sprites
        AudioManager.Instance.ChangeAudioState();
        UpdateAudioButtons();
    }
    //Called when the player click on a shop button
    public void ToggleShopMenu()
    {
        //Make sure that the shop is activated
        shopAnimator.gameObject.SetActive(true);

        //If the shop panel is visible
        if (shopAnimator.GetBool("ShowPanel"))
        {
            //Hide and disable it
            shopAnimator.SetBool("ShowPanel", false);
            StartCoroutine(DisableMenu(shopAnimator, 0.5f));
        }
        //If the shop menu is hidden
        else
        {
            //Update the shop, and move it to the view
            UpdateShopDisplay();
            shopAnimator.SetBool("ShowPanel", true);
        }
    }
    //Called when the player click on the top menu's mission button
    public void ToggleMissionMenu()
    {
        //Change mission menu state
        missionMenuAnimator.SetBool("ShowMissions", !missionMenuAnimator.GetBool("ShowMissions"));

        //Update mission display
        string[] missionTexts = missionManager.GetMissionTexts();
        string[] missionStats = missionManager.GetMissionStats();

        for (int i = 0; i < 3; i++)
        {
            missionPanelElements[i].Find("Mission Text").GetComponent<Text>().text = missionTexts[i];
            missionPanelElements[i].Find("Status Text").GetComponent<Text>().text = missionStats[i];
        }
    }
    //Called, when the player buys an extra speed powerup
    public void BuySpeed(Text priceTag)
    {
        //Obtain price from the pricetag text
        int price = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManager.coinAmmount - price >= 0 && SaveManager.extraSpeed < SaveManager.MAX_SPEED_AMMOUNT)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManager.coinAmmount -= price;
            SaveManager.extraSpeed += 1;
            SaveManager.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("ExtraSpeed");
            UpdateShopDisplay();
        }
    }

    //Called, when the player buys an extra speed powerup
    public void BuyContinue(Text priceTag)
    {
        //Obtain price from the pricetag text
        int price = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManager.coinAmmount - price >= 0 && SaveManager.shield < SaveManager.MAX_BOMB_AMMOUNT)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManager.coinAmmount -= price;
            SaveManager.shield += 1;
            SaveManager.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("ExtraSpeed");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a shield powerup
    public void BuyShield(Text priceTag)
    {
        //Obtain price from the pricetag text
        int price = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManager.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManager.coinAmmount -= price;
            SaveManager.shield += 1;
            SaveManager.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("Shield");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a sonic blast powerup
    public void BuySonicBlast(Text priceTag)
    {
        //Obtain price from the pricetag text
        int price = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManager.coinAmmount - price >= 0 && SaveManager.sonicWave < SaveManager.MAX_BOMB_AMMOUNT)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManager.coinAmmount -= price;
            SaveManager.sonicWave += 1;
            SaveManager.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("SonicBlast");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a revive powerup
    public void BuyRevive(Text priceTag)
    {
        //Obtain price from the pricetag text
        int price = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManager.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManager.coinAmmount -= price;
            SaveManager.revive += 1;
            SaveManager.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("Revive");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys the yellow submarine
    public void BuySubmarine1()
    {
        //Change the current skin ID
        SaveManager.currentSkinID = 0;
        SaveManager.SaveData();

        //Update the player, and the shop display
        playerManager.ChangeSkin(0);
        UpdateShopDisplay();
    }
    //Called, when the player buys the green submarine
    public void BuySubmarine2(Text priceTag)
    {
        //If the submarine is not yet owned
        if (SaveManager.skin2Unlocked == 0)
        {
            //Obtain the price from the pricetag text
            int skin2Price = int.Parse(priceTag.text);

            //If the player can purchase the submarine
            if (SaveManager.coinAmmount - skin2Price >= 0)
            {
                //Decrease coin ammount, and unlock the green submarine
                SaveManager.coinAmmount -= skin2Price;
                SaveManager.skin2Unlocked = 1;
                SaveManager.currentSkinID = 1;
                SaveManager.SaveData();

                ////Update the player, and the shop display
                playerManager.ChangeSkin(1);
                UpdateShopDisplay();
            }
        }
        //If the player already owns the submarine
        else if (SaveManager.currentSkinID != 1)
        {
            //Change the current skin ID
            SaveManager.currentSkinID = 1;
            SaveManager.SaveData();

            //Update the player, and the shop display
            playerManager.ChangeSkin(1);
            UpdateShopDisplay();
        }
    }
    //Called, when the player click on the PlayTrigger
    public void PlayTrigger(Image arrowImage)
    {
        //If the game is not in play mode
        if (!inPlayMode)
        {
            //Set the game to play mode
            inPlayMode = true;
            mainUI.SetActive(true);

            //Hide the main menu
            arrowImage.sprite = arrowSprites[0];
            topMenuAnimator.SetBool("Hide", true);
            //missionMenuAnimator.SetBool("ShowMissions", false);
           // overlayAnimator.SetBool("Visible", false);

            //Start the level
            levelManager.StartLevel();

            //Show the available powerups
            //int[] powerupCount = new int[]{SaveManager.extraSpeed, SaveManager.shield, SaveManager.sonicWave};

            //for (int i = 0; i < powerupCount.Length; i++)
             //   if (powerupCount[i] > 0)
             //       powerupButtons[i].SetBool("Visible", true);

            StartCoroutine(DisableMenu(topMenuAnimator, 1));
        }
    }

    public void setTimeScale(float ts)
    {
        Time.timeScale = ts;
    }
    public void ContinueButton()
    {
        if (SaveManager.shield > 0)
        {
            //Hide the pause menu, and activate the main UI
            //  overlayAnimator.SetBool("Visible", false);
            //pauseMenuAnimator.SetBool("Visible", false);
            float dist = levelGenerator.distance;

            if (pauseMenuAnimator.gameObject.activeSelf)
            {
                pauseMenuAnimator.SetBool("Visible", false);
                StartCoroutine(DisableMenu(pauseMenuAnimator, 0));
            }

            if (finishMenuAnimator.gameObject.activeSelf)
            {
                finishMenuAnimator.SetBool("Visible", false);
                StartCoroutine(DisableMenu(finishMenuAnimator, 0));
            }

            //Reset the game
            powerupManager.Reset();
            levelManager.Restart();

            //Reset the coin ammount
            //collectedCoins = 0;
            currHealth = 100;

            UpdateHealth(currHealth);
            playerManager.health = 100;
            //Activate the main UI
            mainUI.gameObject.SetActive(true);

            //Show available powerup
            int[] powerupCount = new int[] { SaveManager.extraSpeed, SaveManager.shield, SaveManager.sonicWave };

            for (int i = 0; i < powerupCount.Length; i++)
                if (powerupCount[i] > 0)
                    powerupButtons[i].SetBool("Visible", true);
            levelGenerator.distance = dist;
            SaveManager.shield--;
            SaveManager.SaveData();

        }
            
            //StartCoroutine(DisableMenu(pauseMenuAnimator, 0));
        
    }
    //Called, when the playe clicks on the pause button
    public void PauseButton()
    {
        
        pauseMenuAnimator.gameObject.SetActive(true);
        //Set distance and coin text
        pauseDistanceText.text = distanceTraveled + "M";
        pauseCoinText.text = collectedCoins.ToString();
        //If the game is paused
        if (pauseMenuAnimator.GetBool("Visible") == true)
        {
            //Hide the pause menu, and activate the main UI
          //  overlayAnimator.SetBool("Visible", false);
            pauseMenuAnimator.SetBool("Visible", false);
            mainUI.gameObject.SetActive(true);

            //Show the available powerups
            int[] powerupCount = new int[] { SaveManager.extraSpeed, SaveManager.shield, SaveManager.sonicWave };

            for (int i = 0; i < powerupCount.Length; i++)
                if (powerupCount[i] > 0)
                    powerupButtons[i].SetBool("Visible", true);

            //Resume the game
            levelManager.ResumeLevel();
            StartCoroutine(DisableMenu(pauseMenuAnimator, 0));
        }
        //If the game is not paused, and can be paused
        else if (powerupManager.CanUsePowerup())
        {
            // Show the pause menu, and disable the main UI
          //  overlayAnimator.SetBool("Visible", true);
            pauseMenuAnimator.SetBool("Visible", true);
            mainUI.gameObject.SetActive(false);

            //Update pause menu mission texts
            string[] missionTexts = missionManager.GetMissionTexts();
            string[] missionStats = missionManager.GetMissionStats();

            for (int i = 3; i < 6; i++)
            {
                missionPanelElements[i].Find("Mission Text").GetComponent<Text>().text = missionTexts[i - 3];
                missionPanelElements[i].Find("Status Text").GetComponent<Text>().text = missionStats[i - 3];
            }

            //Pause the game
            levelManager.PauseLevel();
        }
    }
    //Called, when the player clicks on a retry button
    public void Retry()
    {
        gameController.destroyAllZomboes();

        //Hide the menus
     //   overlayAnimator.SetBool("Visible", false);

        if (pauseMenuAnimator.gameObject.activeSelf)
        {
            pauseMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(pauseMenuAnimator, 0));
        }

        if (finishMenuAnimator.gameObject.activeSelf)
        {
            finishMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(finishMenuAnimator, 0));
        }
        
        //Reset the game
        powerupManager.Reset();
        levelManager.Restart();

        //Reset the coin ammount
        collectedCoins = 0;
        currHealth = 100;
        playerManager.health = 100;
        //Activate the main UI
        mainUI.gameObject.SetActive(true);

        //Show available powerup
        int[] powerupCount = new int[] { SaveManager.extraSpeed, SaveManager.shield, SaveManager.sonicWave };

        for (int i = 0; i < powerupCount.Length; i++)
            if (powerupCount[i] > 0)
                powerupButtons[i].SetBool("Visible", true);
    }
    //Called, when the player clicks on a quit button
    public void QuitToMain()
    {
        playerManager.stopFire();
        playerManager.shooting = false;
        gameController.destroyAllZomboes();
        //Disable menus
    //    overlayAnimator.SetBool("Visible", false);

        if (pauseMenuAnimator.gameObject.activeSelf)
        {
            pauseMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(pauseMenuAnimator, 0));
        }

        if (finishMenuAnimator.gameObject.activeSelf)
        {
            finishMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(finishMenuAnimator, 0));
        }
        
        //Show top menu
        topMenuAnimator.gameObject.SetActive(true);
        topMenuAnimator.SetBool("MoveDown", false);
        topMenuAnimator.SetBool("Hide", false);

        //Reset the coin ammount
        collectedCoins = 0;
        //
        currHealth = 100;
        playerManager.health = 100;
        gameController.destroyAllZomboes();

        //Reset powerups, and quit to main menu
		powerupManager.Reset();
        levelManager.QuitToMain();
        inPlayMode = false;
    }
    //Receive current distance
    public void UpdateDistance(int newDist)
    {
        distanceTraveled = newDist;
    }
    //Receive collected coins ammount
    public void UpdateCoins(int newCoins)
    {
        collectedCoins = newCoins;
    }
    //Receive collected coins ammount
    public void UpdateHealth(int newHealth)
    {
        currHealth = newHealth;
    }
    
    //Called, when the player collider with a powerup
    public void ShowPowerup(string name)
    {
        //Increase powerup count, and show powerup icon based on the name of the powerup
        switch (name)
        {
            case "ExtraSpeed":
                SaveManager.extraSpeed += 1;
                powerupButtons[0].SetBool("Visible", true);
                break;

            case "Shield":
                SaveManager.shield += 1;
                powerupButtons[1].SetBool("Visible", true);
                break;

            case "SonicBlast":
                SaveManager.sonicWave += 1;
                powerupButtons[2].SetBool("Visible", true);
                break;

            case "Revive":
                SaveManager.revive += 1;
                break;
        }
    }
    //Called, when the player activates a powerup
    public void HidePowerup(Animator anim)
    {
        //If a powerup can't be activated, return
        if (!powerupManager.CanUsePowerup())
            return;

        //Play powerup sound
       // AudioManager.Instance.PlayPowerupUsed();

        //Remove a powerup, and activate it's effect, based on it's name
        switch (anim.gameObject.name)
        {
            case "Speed Button":
                //if (SaveManager.extraSpeed > 0)
                 //   SaveManager.extraSpeed -= 1;
                powerupManager.ExtraSpeed();
                break;

            case "Knife Button":
              //  if (SaveManager.shield > 0)
               //     SaveManager.shield -= 1;
                powerupManager.Shield();
                break;

            case "Bomb Button":
              //  if (SaveManager.sonicWave > 0)
              //      SaveManager.sonicWave -= 1;
                powerupManager.SonicBlast();
                break;
        }

        //Save changes, and hide the powerup button
        SaveManager.SaveData();
        anim.SetBool("Visible", false);
    }
    //Called, when the player activates the revive powerup
    public void RevivePlayer()
    {
        //Remove the used revive
        SaveManager.revive -= 1;
        SaveManager.SaveData();

        //Revive the player
       // AudioManager.Instance.PlayRevive();
        powerupManager.Revive();
        levelManager.ReviveUsed();
        StopCoroutine("Revive");

        //Hide revive button
        powerupButtons[3].SetBool("Visible", false);
    }
    //Called, after the player has crashed
    public void ShowCrashScreen(int distance)
    {
        //If the player has a revive, show it
       // if (SaveManager.revive > 0)
       //     StartCoroutine("Revive");
        //Else, show the finish menu
       // else
        UpdateCrashScreenDisplay();
        AudioManager.Instance.PlayMusicMenu();
        gameController.destroyAllZomboes();
        gameController.StopSpawnZomboes();
        ShowFinishMenu();
        Time.timeScale = 0;
    }
    //Called, when a mission is completed
    public void ShowMissionComplete(string text)
    {
        //Find the first unused mission notificator, and show it
      /*  for (int i = 0; i < 3; i++)
        {
            if (!usedMissionNotifications[i])
            {
                usedMissionNotifications[i] = true;

                missionNotifications[i].transform.Find("Text").GetComponent<Text>().text = text;
                StartCoroutine(MissionNotificationCountdown(missionNotifications[i], "Pos" + (i + 1), i));

                return;
            }
        }*/
    }
    //Updates the sprite of the audio buttons
    private void UpdateAudioButtons()
    {
        Sprite s = AudioManager.Instance.audioEnabled == true ? audioSprites[0] : audioSprites[1];

        foreach (Image item in audioButtons)
            item.sprite = s;
    }

    public void UpdateBestDistance()
    {
        bestDistanceText.text = SaveManager.bestDistance.ToString() + "M";
    }

    private void UpdateCrashScreenDisplay()
    {
        currContAmmTextCS.text = SaveManager.shield.ToString();
    }

    //Updates the shop display texts
    private void UpdateShopDisplay()
    {
        //Update texts
        coinAmmount.text = SaveManager.coinAmmount.ToString();
        currSpeedAmmText.text = SaveManager.extraSpeed.ToString();
        currBombAmmText.text = SaveManager.sonicWave.ToString();
        currContAmmText.text = SaveManager.shield.ToString();
        /*
        ShopOwnedItems[0].text = SaveManager.extraSpeed.ToString();
        ShopOwnedItems[1].text = SaveManager.shield.ToString();
        ShopOwnedItems[2].text = SaveManager.sonicWave.ToString();
        ShopOwnedItems[3].text = SaveManager.revive.ToString();
        */
        //If the yellow submarine is active
       /* if (SaveManager.currentSkinID == 0)
        {
            //Set button sprite
            shopSubmarineButtons[0].sprite = ShopSkinButtonSprites[0];

            //Set sprite for button 2
            if (SaveManager.skin2Unlocked == 1)
                shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[1];
            else
                shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[2];
        }
        //If the green submarine is active
        else
        {
            //Set button sprites
            shopSubmarineButtons[0].sprite = ShopSkinButtonSprites[1];
            shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[0];
        }*/
    }
    //Shows the finish menu
    private void ShowFinishMenu()
    {
        //Disable main UI, and show the finish menu
        mainUI.gameObject.SetActive(false);
        //overlayAnimator.SetBool("Visible", true);
        finishMenuAnimator.gameObject.SetActive(true);
        finishMenuAnimator.SetBool("Visible", true);

        //
        gameController.StopSpawnZomboes();
        gameController.destroyAllZomboes();
        //Set mission texts
       /* string[] missionTexts = missionManager.GetMissionTexts();
        string[] missionStats = missionManager.GetMissionStats();

        for (int i = 6; i < 9; i++)
        {
            missionPanelElements[i].Find("Mission Text").GetComponent<Text>().text = missionTexts[i - 6];
            missionPanelElements[i].Find("Status Text").GetComponent<Text>().text = missionStats[i - 6];
        }
        */
        //Set distance and coin text
        finishDistanceText.text = distanceTraveled + "M";
        finishCoinText.text = collectedCoins.ToString();

        //Add collected coins to total coins
        if (SaveManager.bestDistance < levelGenerator.CurrentDistance())
            SaveManager.bestDistance = levelGenerator.CurrentDistance();
        SaveManager.coinAmmount += collectedCoins;
        SaveManager.SaveData();
        //SaveManager.coinAmmount = 150000;
        //SaveManager.sonicWave = 5;
        //SaveManager.extraSpeed = 5;
        //SaveManager.SaveData();
        //collectedCoins = 0;
    }
    //Returns true, if the game is in play mode
    public bool InPlayMode()
    {
        return inPlayMode;
    }
    //Converts a number to a string, with a given digit number. For example, this turns 4 to "0004"
    private string AddDigitDisplay(int number, int digits)
    {
        string s = "";

        for (int i = 0; i < digits - number.ToString().Length; i++)
            s += "0";

        s += number.ToString();

        return s;
    }
    //Shows a mission notificator for 2 seconds, then hides it
    private IEnumerator MissionNotificationCountdown(Animator missionNotification, string boolName, int arrayID)
    {
        missionNotification.SetBool(boolName, true);
        yield return new WaitForSeconds(2);
        missionNotification.SetBool(boolName, false);
        usedMissionNotifications[arrayID] = false;
    }
    //Shows the revive button for 3 seconds
    private IEnumerator Revive()
    {
        powerupButtons[3].SetBool("Visible", true);
        yield return new WaitForSeconds(3);

        powerupButtons[3].SetBool("Visible", false);
        ShowFinishMenu();
    }
    //Disables a specific menu after a given time
    private IEnumerator DisableMenu(Animator menu, float time)
    {
        yield return new WaitForSeconds(time);
        menu.gameObject.SetActive(false);
    }
}