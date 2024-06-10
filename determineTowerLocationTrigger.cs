using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class determineTowerLocationTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            var b = (movement.towerLocation)Int32.Parse(this.name);
            FindObjectOfType<movement>().currentLocation = b;
        }

    }
}
