using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour 
{
	public float destroyTime = 5;
	
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
}
