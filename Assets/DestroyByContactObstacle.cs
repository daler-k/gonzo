using UnityEngine;
using System.Collections;
//using Holoville.HOTween;

public class DestroyByContactObstacle : MonoBehaviour
{
    public GameObject explosionObstacle;
    public GameObject explosionHole;
   

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Obstacle")
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            Destroy(Instantiate(explosionObstacle, other.transform.position, other.transform.rotation), 1f);
            StartCoroutine(SetActiveGO(other.gameObject));
        }
        else if (other.tag == "Hole")
        {
            //Destroy(other.gameObject);
            GameObject go = Instantiate(explosionHole, other.transform.position, other.transform.rotation) as GameObject;
            go.transform.parent = other.transform;
            Destroy(go, 1f);
        }

    }

    IEnumerator SetActiveGO(GameObject go)
    {
        yield return new WaitForSeconds(1);
        go.SetActive(true);
    }


}

