using UnityEngine;
using System.Collections;

public class EvasiveManeuverEnd : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;

    private Vector3 moveDirection;
    public GameObject endPoint;


    void Start()
    {
        moveDirection = Vector3.up;
    }

  

    void Update()
    {
        // 1
        Vector3 currentPosition = gameObject.transform.position;
        // 2
        //if (Input.GetButton("Fire1"))
        //{

        //Debug.Log("FIREEE");
        // 3
        Vector3 moveToward = endPoint.transform.position;
        //Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 4
        moveDirection = moveToward - currentPosition;
        moveDirection.z = 0;
        moveDirection.Normalize();
        // }

        Vector3 target = moveDirection * moveSpeed + currentPosition;
        gameObject.transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

        float targetAngle = -Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation =
            Quaternion.Slerp(gameObject.transform.rotation,
                             Quaternion.Euler(0, 0, targetAngle),
                             turnSpeed * Time.deltaTime);

      
    }
}
