using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTriggerCamera : MonoBehaviour
{
    [SerializeField]
    private FollowPlayer2 fp2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fp2.atTheStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            fp2.atTheStart = false;
        }
    }
}
