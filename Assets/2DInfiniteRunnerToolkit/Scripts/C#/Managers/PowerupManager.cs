using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour 
{
    public LevelGenerator levelGenerator;                       //A link to the Level Generator
    public PlayerManager playerManager;                         //A link to the Player Manager
    public SonicBlast sonicBlast;                               //A link to the Sonic Blast
    public GameObject shield;

    public float extraSpeedFactor;                              //The scrolling speed during the Extra Speed powerup
    public float extraSpeedLength;                              //The duration of the extra speed powerup

    private bool powerupUsed;                                   //True, if the player used a powerup in this run
    private bool paused;                                        //True, if the level is paused
    public static bool toDestroyAllZomboes;
    public GameObject pengui;

    public GameObject knifeButton;
    public GameObject bombButton;
    public GameObject speedButton;
    public GonzoAnimator gonzoAnimator;
    public GUIManager guiManager;
    public AudioManager audioManager;

    private int bombAmm = 5;
    private int speedAmm = 5;

    public bool isExtraSpeedActive = false;
    public bool isBombActive = false;

    // Used for initialization
    void Start()
    {
        powerupUsed = false;
        paused = false;
        toDestroyAllZomboes = false;
    }

    //Changes pause state to state
    public void SetPauseState(bool state)
    {
        paused = state;
        sonicBlast.SetPauseState(state);
    }
    //Return true, if a powerup can be used
    public bool CanUsePowerup()
    {
        return playerManager.CanUsePowerup();
    }
    //Returns true, if a powerup was used
    public bool PowerupUsed()
    {
        return powerupUsed;
    }
    //Resets the variables
    public void Reset()
    {
        powerupUsed = false;
        paused = false;
    }

    //Activate the extra speed powerup
    public void ExtraSpeed()
    {
        iTween.ShakeScale(speedButton, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 5f));
        if (SaveManager.extraSpeed > 0)
        {
            if (!isExtraSpeedActive)
                audioManager.PlayPowerUpSpeed();
            SaveManager.extraSpeed--;
            powerupUsed = true;
            isExtraSpeedActive = true;
            StartCoroutine("ExtraSpeedEffect");

            // set new speed for active zomboes 
            GameObject[] zomboes = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject zombo in zomboes)
            {
                if (zombo.name != "zombie1" && zombo.name != "zombie2" && zombo.name != "zombie3") // save prefabs in the scene for instantiating
                    zombo.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
            }
            zomboes = null;
        }

        
    }
    //Activate the extra speed powerup
    public void Shield()
    {
        //powerupUsed = true;
        

        if (!gonzoAnimator.isActive)
        {
            audioManager.PlayPowerUpKnife();
            //guiManager.setTimeScale(0.5f);
            iTween.ShakeScale(knifeButton, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 5f ));
            //PulseUp();
            StartCoroutine(SetActiveKnife());
            playerManager.RaiseShield();
            gonzoAnimator.activateKnife();
        }
        //-----------------------
        //shield.SetActive(true);
       // StartCoroutine("SetActiveShield");
        //Debug.Log("SHHIEEEELD");
    }
    public IEnumerator SetActiveKnife()
    {
        Debug.Log("COLOR");
        Color32 _color = knifeButton.GetComponent<CanvasRenderer>().GetColor();
        knifeButton.GetComponent<CanvasRenderer>().SetColor(Color.red);
        yield return new WaitForSeconds(5f);
        knifeButton.GetComponent<CanvasRenderer>().SetColor(_color);
        gonzoAnimator.isActive = false;
    }

    //Activate the extra speed powerup
    public void SonicBlast()
    {

        iTween.ShakeScale(bombButton, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 5f));
        if (SaveManager.sonicWave > 0 && !isBombActive)
        {
                audioManager.PlayPowerUpBomb();
        SaveManager.sonicWave--;
        isBombActive = true;
        //guiManager.setTimeScale(0.3f);
        //powerupUsed = true;
       sonicBlast.gameObject.SetActive(true);
        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0f, 0.5f, 0f), 2f);
        //Invoke("setCameraPosition", 1f);
       // toDestroyAllZomboes = true;
        sonicBlast.Activate();
        }
    }


    public void setCameraPosition()
    {
        Camera.main.gameObject.transform.position = new Vector3(pengui.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
    }
    //Revives the player, and launches a Sonic Blast
    public void Revive()
    {
        powerupUsed = true;

        sonicBlast.gameObject.SetActive(true);
        sonicBlast.Activate();
    }

    //Activates the extra speed for the given duration, then disables it
    private IEnumerator ExtraSpeedEffect()
    {
        levelGenerator.StartExtraSpeed(extraSpeedFactor);
        playerManager.ActivateExtraSpeed();

        //Declare variables, get the starting position, and move the object
        float i = 0.0f;
        float rate = 1.0f / extraSpeedLength;

        while (i < 1.0)
        {
            //If the game is not paused, increase t
            if (!paused)
                i += Time.deltaTime * rate;

            //Wait for the end of frame
            yield return 0;
        }

        levelGenerator.EndExtraSpeed();
        playerManager.DisableExtraSpeed();
        isExtraSpeedActive = false;

    }
}
