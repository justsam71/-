using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 50f;
    public float sidewaysForce = 65;
    public Transform player;
    bool topTriggered = false;

    public towerLocation currentLocation = 0;
    public enum towerLocation
    {
        Start,
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
        Finish
    }

    public void PlayerReachedTheTop()
    {
        topTriggered = true; 
    }
    public void PlayerFell()
    {
        topTriggered = false; //because a player fell from the top  
    }


    void FixedUpdate()
    {
        //the end of the game if player falls
        rb.drag = 1; //??
        if (rb.position.y < 0f)
        {
            forwardForce = 0f;
            sidewaysForce = 0f;
            FindObjectOfType<GameManager>().EndGame();
        }

        if (topTriggered == false) //if a player hasn't reached the top or fell from it
        {

            //the initial control
            if ((int)currentLocation == 0)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(0, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(0, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            // 1/12 to the right
            if ((int) currentLocation == 1)
            {
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime *  1/4, ForceMode.VelocityChange);
                    Debug.Log("1/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 1 / 4, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 1 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            //second 2/12 diagonally a little right and forward
            if ((int)currentLocation == 2)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 3 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 3 / 4, 0, -forwardForce* Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime , ForceMode.VelocityChange);
                    Debug.Log("2/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime , ForceMode.VelocityChange);
                }
            }

            // 3/12 right and forward
            if ((int)currentLocation == 3)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime , 0, -forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime * 1 / 4, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("3/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime * 1 / 4, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            //  4/12 diagonally left and forward
            if ((int)currentLocation == 4)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, -forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime * 1 / 4, 0, -sidewaysForce * Time.deltaTime , ForceMode.VelocityChange);
                    Debug.Log("4/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime * 1 / 4, 0, sidewaysForce * Time.deltaTime , ForceMode.VelocityChange);
                }
            }

            //  5/12 left
            if ((int)currentLocation == 5)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime , 0, -forwardForce * Time.deltaTime * 3 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 3 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("5/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            // 6/12 diagonally left and back
            if ((int)currentLocation == 6)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 1 / 4, 0, -forwardForce * Time.deltaTime , ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 1 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime , 0, sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                    Debug.Log("6/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
            }

            // 7/12 back
            if ((int)currentLocation == 7)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 1 / 4, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 1 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                    Debug.Log("7/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
            }

            //8/12 diagonally back and to the right
            if ((int)currentLocation == 8)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 3 / 4, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 3 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("8/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            //9/12 diagonally back and to the right
            if ((int)currentLocation == 9)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, -forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime * 1 / 4, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("9/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime * 1 / 4, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            //10/12 diagonally back and to the right
            if ((int)currentLocation == 10)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime, 0, -forwardForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime * 1 / 4, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("10/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime * 1 / 4, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            //11/12 diagonally back and to the right
            if ((int)currentLocation == 11)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, forwardForce * Time.deltaTime * 3 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime, 0, -forwardForce * Time.deltaTime * 3 / 4, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("11/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            //12/12 diagonally back and to the right
            if ((int)currentLocation == 12)
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(forwardForce * Time.deltaTime * 1 / 4, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-forwardForce * Time.deltaTime * 1 / 4, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, -sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                    Debug.Log("12/12");
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, sidewaysForce * Time.deltaTime * 1 / 4, ForceMode.VelocityChange);
                }
            }
        }
        else // player reaches the top
        {
            forwardForce = 40f;
            sidewaysForce = 40f;
            rb.drag = 2;
            if (Input.GetKey("s"))
            {
                rb.AddForce(-forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (Input.GetKey("w"))
            {
                rb.AddForce(forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                Debug.Log("top");
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(0, 0, -sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce(0, 0, sidewaysForce * Time.deltaTime, ForceMode.VelocityChange);
            }
     
        }
    }
    
}

