using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleTrigger : MonoBehaviour
{
    public Rigidbody apple1;
    public Rigidbody apple2;
    public Rigidbody apple3;
    public Rigidbody apple4;
    public Rigidbody apple5;
    public Rigidbody apple6;
    public Rigidbody apple7;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("hi");
            apple1.useGravity = true;
            apple2.useGravity = true;
            apple3.useGravity = true;
            apple4.useGravity = true;
            apple5.useGravity = true;
            apple6.useGravity = true;
            apple7.useGravity = true;
        }
 
    }
}
