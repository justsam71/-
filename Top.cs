using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            gm.Reached(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("Reached", 0.5f);
        }
    }

    void Reached()
    {
        gm.Reached(false);
    }
}
