using UnityEngine;
using System.Collections;

public class HeadRotator : MonoBehaviour
{

    public float time = 0f;
    private float startScaleX;

    private float startScaleY;


	// Use this for initialization
	void Start () {
        PulseUp();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void PulseUp()
    {
       // iTween.ShakeScale(gameObject, iTween.Hash("x", .02f, "y", .02f, "time", 5f));
       // iTween.ScaleTo(gameObject, iTween.Hash("x", 0.35f, "y", 0.35f, "time", 1.5f, "easeType", iTween.EaseType.linear));
        iTween.RotateTo(gameObject, iTween.Hash("z", 15f, "time", time, "easeType", iTween.EaseType.linear));
        Debug.Log("pulseUP");
       // iTween.ScaleTo(gameObject, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 1.5f));
        Invoke("PulseDown", time);
    }
    private void PulseDown()
    {
        // iTween.ShakeScale(gameObject, iTween.Hash("x", .02f, "y", .02f, "time", 5f));
        //iTween.ScaleTo(gameObject, iTween.Hash("x", 0.35f, "y", 0.35f, "time", 1.5f));

        iTween.RotateTo(gameObject, iTween.Hash("z", -45f, "time", time, "easeType", iTween.EaseType.linear));

        Invoke("PulseUp", time);
    }
}
