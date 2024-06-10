using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botMovement : MonoBehaviour
{
    public int speed;
    public float sideSpeed;
    public Rigidbody rb;
    public Transform targetForward;
    public Transform centerTarget;
    public GameObject start;

    private void Start()
    {
        rb.position = start.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 forwardVector = (targetForward.position - transform.position);
        Vector3 centerVector = (centerTarget.position - transform.position);

        //Debug.Log();
        rb.AddForce(forwardVector.normalized * speed * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddForce(centerVector.normalized * sideSpeed * Time.deltaTime, ForceMode.VelocityChange);

        if (rb.position.y < -17)
        {
            rb.position = start.transform.position;
        }
        if (rb.position.y > 30)
        {
            rb.position = start.transform.position;   
        }
    }
}
