using UnityEngine;
using System.Collections;

public class CloudsMoving : MonoBehaviour {
	public bool left2right = true;
    public float positionLimit = 4.0f;
	public  float step = 0.003f;
	
	void Start() {
		//if(Screen.width == 1136)
			//positionLimit = 3.0f;
		//else
			//positionLimit = 4.0f;
	}
	
	void Update() {
		
		if (left2right && this.transform.position.x < positionLimit)
            this.transform.position = new Vector3(this.transform.position.x + step, this.transform.position.y, this.transform.position.z + Mathf.Sin(this.transform.position.x) / 1200);
		else if (!left2right && this.transform.position.x > -positionLimit)
            this.transform.position = new Vector3(this.transform.position.x - step, this.transform.position.y, this.transform.position.z + Mathf.Sin(this.transform.position.x) / 1200);
		else if (this.transform.position.x >= positionLimit || this.transform.position.x <= -positionLimit)
			left2right = !left2right;
	}
}