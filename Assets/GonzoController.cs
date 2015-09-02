using UnityEngine;
using System.Collections;

public class GonzoController : MonoBehaviour {

    public float positionDamping = 5.0f;
    Vector3 wantedPosition = new Vector3(0.0f, 0.0f, 0.0f);

    void FixedUpdate () {
        
        if (Input.touchCount > 0 && 
          Input.GetTouch(0).phase == TouchPhase.Moved) {
            // Get movement of the finger since last frame
              Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
           
                        if(touchDeltaPosition.x > 8.0f && transform.position.x >= -0.5f)
                        {
                                wantedPosition.x = 3f;
                        }
                        if(touchDeltaPosition.x > 8.0f && transform.position.x <= -0.5f)
                        {
                                wantedPosition.x = 0.0f;
                        }
                        if(touchDeltaPosition.x < -8.0f && transform.position.x >= 0.5f)
                        {
                                wantedPosition.x = 0.0f;
                        }
                        
                        if(touchDeltaPosition.x < -8.0f && transform.position.x <= 0.5f)
                        {
                                wantedPosition.x = -3f;
                        }
            
                       // touchDeltaPosition2 = touchDeltaPosition;
            
            }

        Vector3 myPosition = transform.position;
        myPosition = Vector3.Lerp(myPosition, wantedPosition, positionDamping * Time.deltaTime);
        transform.position = new Vector2(myPosition.x, myPosition.y);
    }
}
