using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
    public AudioSource musicPlayer;
    public AudioSource effectPlayer;
    public AudioSource zomboPlayer;
    public AudioSource powerupPlayer;
    public AudioSource zomboHitPlayer;
    public AudioSource zomboDiedPlayer;
    public AudioSource obstaclePlayer;

    public AudioClip menuClickClip;
    public AudioClip missionCompleteClip;

    public AudioClip coinCollectedClip;
    public AudioClip explosionClip;

    public AudioClip powerupCollectedClip;
    public AudioClip powerupUsedClip;
    public AudioClip reviveClip;

    public AudioClip knifeP;
    public AudioClip bombP;
    public AudioClip speedP;
    public AudioClip[] greenHole;
    public AudioClip blackHole;
    public AudioClip[] heroHurt;

    public AudioClip shoot;

    public AudioClip musicMenu;
    public AudioClip musicGame;

    public AudioClip[] zomboVoices;
    public AudioClip[] zomboHitVoices;
    public AudioClip[] zomboDiedVoices;

    public bool audioEnabled;

    static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }


	// Use this for initialization
	void Start () 
    {
        instance = this;
		
        if (SaveManager.audioEnabled == 1)
        {
            audioEnabled = true;

            if (musicPlayer)
                musicPlayer.Play();
        }
        else
        {
            audioEnabled = false;
        }
	}

    public void ChangeAudioState()
    {
        if (audioEnabled)
        {
            audioEnabled = false;
            SaveManager.audioEnabled = 0;

            if (musicPlayer)
                musicPlayer.Stop();

            if (effectPlayer)
                effectPlayer.Stop();
        }
        else
        {
            audioEnabled = true;
            SaveManager.audioEnabled = 1;

            if (musicPlayer)
                musicPlayer.Play();
        }

        SaveManager.SaveData();
    }

    public void PlayMenuClick()
    {
        if (menuClickClip && audioEnabled)
        {
            effectPlayer.clip = menuClickClip;
            effectPlayer.Play();
        }
    }

    public void PlayPowerUpKnife()
    {
        if (knifeP && audioEnabled)
        {
            powerupPlayer.clip = knifeP;
            powerupPlayer.Play();
        }
    }

    public void PlayPowerUpBomb()
    {
        if (bombP && audioEnabled)
        {
            powerupPlayer.clip = bombP;
            powerupPlayer.Play();
        }
    }

    public void PlayPowerUpSpeed()
    {
        if (speedP && audioEnabled)
        {
            powerupPlayer.clip = speedP;
            powerupPlayer.Play();
        }
    }

    public void PlayShoot()
    {
        if (shoot && audioEnabled)
        {
            effectPlayer.clip = shoot;
            effectPlayer.Play();
        }
    }

    public void PlayMissionComplete()
    {
        if (missionCompleteClip && audioEnabled)
        {
            effectPlayer.clip = missionCompleteClip;
            effectPlayer.Play();
        }
    }
    public void PlayCoinCollected()
    {
        if (coinCollectedClip && audioEnabled)
        {
            obstaclePlayer.clip = coinCollectedClip;
            obstaclePlayer.Play();
        }
    }
    public void PlayExplosion()
    {
        if (explosionClip && audioEnabled)
        {
            obstaclePlayer.clip = explosionClip;
            obstaclePlayer.Play();
        }
    }
    public void PlayPowerupCollected()
    {
        if (powerupCollectedClip && audioEnabled)
        {
            effectPlayer.clip = powerupCollectedClip;
            effectPlayer.Play();
        }
    }
    public void PlayPowerupUsed()
    {
        if (powerupUsedClip && audioEnabled)
        {
            effectPlayer.clip = powerupUsedClip;
            effectPlayer.Play();
        }
    }
    public void PlayRevive()
    {
        if (reviveClip && audioEnabled)
        {
            effectPlayer.clip = reviveClip;
            effectPlayer.Play();
        }
    }
    public void PlayZombo()
    {
        if (audioEnabled)
        {
            zomboPlayer.clip = zomboVoices[Random.Range(0,2)];
            zomboPlayer.Play();
        }
    }

    public void PlayZomboHit()
    {
        if (audioEnabled)
        {
            zomboHitPlayer.clip = zomboHitVoices[Random.Range(0, 2)];
            zomboHitPlayer.Play();
        }
    }


   
    public void PlayZomboDied()
    {
        if (audioEnabled)
        {
            zomboDiedPlayer.clip = zomboDiedVoices[Random.Range(0, 1)];
            zomboDiedPlayer.Play();
        }
    }

    public void PlayHeroHurt()
    {
        if (audioEnabled)
        {
            obstaclePlayer.clip = heroHurt[Random.Range(0, 1)];
            obstaclePlayer.Play();
        }
    }

    public void PlayGreenHole()
    {
        if (audioEnabled)
        {
            obstaclePlayer.clip = greenHole[Random.Range(0, 3)]; ;
            obstaclePlayer.Play();
        }
    }

    public void PlayMusicMenu()
    {
        if (musicMenu && audioEnabled)
        {
            musicPlayer.clip = musicMenu;
            musicPlayer.Play();
        }
    }

    public void PlayMusicGame()
    {
        if (musicGame && audioEnabled)
        {
            musicPlayer.clip = musicGame;
            musicPlayer.Play();
        }
    }



}
