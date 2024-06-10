using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactitePoint : MonoBehaviour
{
    [SerializeField]
    private GameObject stalactite;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().Respawn();
            Debug.Log("1");
        }
        else if (other.tag == "SmallRock")
        {
            other.GetComponent<BreakableUponCollision>().Break();
            Debug.Log("2");
        }

    }

    private void Update()
    {
        if (transform.position.y <= -30)
        {
            Destroy(stalactite);
        }
    }
}
