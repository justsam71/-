using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMovement : MonoBehaviour
{
    //public GameObject PlayerParentRotate; 
    public Transform target; // (dont look at me /// center)
    public Transform targetForward; // (forward (round))
    public Transform targetCircleWayForward;
    public Transform targetCircleWayBackward;

    public Rigidbody rb;
    public float forwardForce = 50f;
    public float sidewaysForce = 65;
    bool topTriggered = false;

    private float k = 0;
    private float toCircleDistance = 0;
    


    private void Start()
    {
        //rb.drag = 0.8f;
    }

    private void FixedUpdate()
    {
        //переделать! плохо для оптимизации
        topTriggered = FindObjectOfType<GameManager>().ReachedTheTop;
        //startTriggered = FindObjectOfType<GameManager>().AtTheStart;

        if (rb.position.y < 0f)
        {
            FindObjectOfType<GameManager>().Respawn(); //сделать ссылку на gm
        }

        if (topTriggered)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(0, 0, forwardForce / 2 * Time.deltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(0, 0, -forwardForce / 2 * Time.deltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(-forwardForce / 2 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(forwardForce / 2 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }
        else
        {
            Vector3 centerVector = (target.position - transform.position);
            Vector3 forwardVector = (targetForward.position - transform.position);
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(forwardVector.normalized * forwardForce * Time.deltaTime, ForceMode.VelocityChange);

                if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
                {
                    Vector3 circlewayVector = (targetCircleWayForward.position - transform.position);
                    toCircleDistance = Vector3.Distance(transform.position, targetCircleWayForward.position);
                    if (toCircleDistance < 0.5)
                        k = 0.25f;
                    if (toCircleDistance < 1)
                        k = 0.5f;
                    else if (toCircleDistance < 2)
                        k = 1;
                    else if (toCircleDistance < 5)
                        k = 1.5f;
                    else 
                        k = 2;
                    rb.AddForce(circlewayVector.normalized * forwardForce * k * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(-forwardVector.normalized * forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
                {
                    Vector3 circlewayBackwardVector = (targetCircleWayBackward.position - transform.position);
                    toCircleDistance = Vector3.Distance(transform.position, targetCircleWayBackward.position);
                    if (toCircleDistance < 0.5)
                        k = 0.5f;
                    else if (toCircleDistance < 2)
                        k = 1;
                    else if (toCircleDistance < 5)
                        k = 1.5f;
                    else
                        k = 2;

                    rb.AddForce(circlewayBackwardVector.normalized * forwardForce * k * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(centerVector.normalized * sidewaysForce * 1.2f * Time.deltaTime, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(-centerVector.normalized * sidewaysForce * Time.deltaTime, ForceMode.Impulse);
            }
            //if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            //{
            //    rb.AddForce(centerVector.normalized * sidewaysForce * 0.6f * Time.deltaTime, ForceMode.Impulse);
            //}
            //else
            //{
            //    rb.AddForce(-centerVector.normalized * sidewaysForce * Time.deltaTime, ForceMode.Impulse);
            //}
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(0, -forwardForce / 2 * Time.deltaTime, 0, ForceMode.VelocityChange);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 1.5f, 0);

            }
        }

    }
}

