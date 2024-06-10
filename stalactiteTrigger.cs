using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactiteTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject stalactite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stalactite.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
            Destroy(this.gameObject);
        }
    }

}
