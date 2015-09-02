using UnityEngine;
using System.Collections;

public class GonzoAnimator : MonoBehaviour
{

    public Sprite[] sprites;
    public float framesPerSecond;

    public GameObject head;
    public GameObject gonzo;
    public GameObject gun;
    public GameObject shot;
    public PlayerManager playerManager;

    public GameObject knifeRight;
    public GameObject knifeLeft;
    public GameObject knifeStatic;

    private SpriteRenderer spriteRenderer;

    public bool isActive;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
        index = index % sprites.Length;
        spriteRenderer.sprite = sprites[index];*/  
        if (!isActive)
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void activateKnife()
    {

        playerManager.stopFire();
        knifeStatic.SetActive(false);
        gun.gameObject.SetActive(false);
        knifeLeft.SetActive(true);
        knifeRight.SetActive(true);
        isActive = true;
        iTween.ShakeRotation(gameObject, iTween.Hash("z", 270, "time", 1f, "easeType", iTween.EaseType.punch));
        iTween.PunchRotation(head, iTween.Hash("z", 360, "time", 1f, "easeType", iTween.EaseType.easeOutBack));
        iTween.PunchScale(gonzo, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 1f, "easeType", iTween.EaseType.easeOutBounce));
        //framesPerSecond = 12.0f;
        Invoke("deactivateKnife", 1.01f);
    }

    public void deactivateKnife()
    {
        //framesPerSecond = 0.0f;
       // spriteRenderer.sprite = sprites[0];
        knifeLeft.SetActive(false);
        knifeRight.SetActive(false);
        knifeStatic.SetActive(true);
        gun.gameObject.SetActive(true);
        playerManager.shooting = false;
       // isActive = false;

        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
