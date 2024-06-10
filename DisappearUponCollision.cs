using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearUponCollision : MonoBehaviour
{
    [SerializeField]
    private float appearTime;

    private void Start()
    {
        if (this.gameObject.tag == "firstFinalPlatform")
        {
            this.GetComponent<MeshRenderer>().material = FindObjectOfType<LevelGenerator2>().finalPlatformMaterial; //findObject может замедлять игру!!!
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            Invoke("Back", appearTime);
        }
    }

    void Back()
    {
        this.gameObject.SetActive(true);
    }
}
