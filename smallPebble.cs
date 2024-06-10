using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallPebble : MonoBehaviour
{
    [SerializeField]
    private GameObject fallingPebbleParticle;
    private GameObject particle;
    [SerializeField]
    private Transform particleSpawner;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            particle = Instantiate(fallingPebbleParticle);
            particle.transform.position = particleSpawner.position;
            particle.transform.SetParent(this.transform);

            Destroy(particle, 2);
        }
    }
}
