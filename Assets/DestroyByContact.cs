using UnityEngine;
using System.Collections;
//using Holoville.HOTween;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    public AudioManager audioManager;
    public GameObject GO;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Boundary")
        {
            return;
        }

        if (explosion != null)
        {
            //Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.gameObject.name == "zombie1(Clone)" || other.gameObject.name == "zombie2(Clone)" || other.gameObject.name == "zombie3(Clone)")
        {
            audioManager.PlayZomboHit();
            other.gameObject.name = "Hurt";
            iTween.ShakeScale(other.gameObject, iTween.Hash("x", 0.8f, "y", 0.5f, "time", 0.3f));
            Destroy(Instantiate(explosion, other.transform.position, other.transform.rotation), 1f);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.name == "Hurt" )
        {
                audioManager.gameObject.transform.parent = GO.transform;
                audioManager.PlayZomboDied();
                //Destroy(asGO);
                //PlayerController.touch_counter++;
                //if (PlayerController.touch_counter == 6.0f)
                //{
                //gameController.AddHP(20);
                Destroy(Instantiate(playerExplosion, other.transform.position, other.transform.rotation), 1f);
                Destroy(Instantiate(explosion, other.transform.position, other.transform.rotation), 1f);
                //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

                //     gameController.GameOver();
                //     Done_PlayerController.touch_counter = 0.0f;
                Destroy(other.gameObject);
                //GameController.currZombCount--;
                Destroy(this.gameObject);
                //    HOTween.Shake(Camera.current, 3f, "fieldOfView", 100f);
                //}
                // else
                // {
                //     if (Done_PlayerController.touch_counter != 5.0f)
                //         gameController.AddHP(20);
                //      HOTween.Shake(Camera.current, 0.5f, "fieldOfView", 20f);

                //  }
        }
       // gameController.AddScore(scoreValue);

    }


}

