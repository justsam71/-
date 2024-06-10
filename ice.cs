using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || SnowAnomalies.ice)
        {
            collision.gameObject.GetComponent<Rigidbody>().drag = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || SnowAnomalies.ice)
        {
            collision.gameObject.GetComponent<Rigidbody>().drag = 1;
        }
    }
}
