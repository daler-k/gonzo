using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name != "SonicBlast")
            Destroy(other.gameObject);
    }
}