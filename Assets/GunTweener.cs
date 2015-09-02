using UnityEngine;
using System.Collections;

public class GunTweener : MonoBehaviour
{

    public float time = 0f;
    private float startScaleX;

    private float startScaleY;


	// Use this for initialization
	void Start () {
        startScaleX = gameObject.transform.localScale.x;
        startScaleY = gameObject.transform.localScale.y;
        PulseUp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void PulseUp()
    {
       // iTween.ShakeScale(gameObject, iTween.Hash("x", .02f, "y", .02f, "time", 5f));
       // iTween.ScaleTo(gameObject, iTween.Hash("x", 0.35f, "y", 0.35f, "time", 1.5f, "easeType", iTween.EaseType.linear));
        iTween.ScaleTo(gameObject, iTween.Hash("x", startScaleX + 0.1f, "y", startScaleY + 0.1f, "time", time, "easeType", iTween.EaseType.linear));

       // iTween.ScaleTo(gameObject, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 1.5f));
        Invoke("PulseDown", time);
    }
    private void PulseDown()
    {
        // iTween.ShakeScale(gameObject, iTween.Hash("x", .02f, "y", .02f, "time", 5f));
        //iTween.ScaleTo(gameObject, iTween.Hash("x", 0.35f, "y", 0.35f, "time", 1.5f));

        iTween.ScaleTo(gameObject, iTween.Hash("x", startScaleX, "y", startScaleY, "time", time, "easeType", iTween.EaseType.linear));
        Invoke("PulseUp", time);
    }
}
