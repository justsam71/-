using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpButton : MonoBehaviour
{
    [SerializeField]
    private float knockbackForce = 15;
    [SerializeField]
    private float cooldownTime = 0.5f;
    private float multi = 1;
    private int hits = 0;
    [SerializeField]
    private string direction = "up";
    [SerializeField]
    private MeshRenderer MR;
    [SerializeField]
    private Material[] mats;
    private bool cooldown = false;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (playerRigidbody != null && !cooldown)
        {
            hits++;
            if (hits % 3 == 2) //каждый третий
            {
                ChangeMat();
            }
            else if (hits % 3 == 0)
            {
                multi = 1.75f;
                ChangeMatBack();
            }
            if (direction == "up")
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, knockbackForce * multi, playerRigidbody.velocity.z);
            else if (direction == "down")
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -knockbackForce * multi, playerRigidbody.velocity.z);          
            cooldown = true;
            Invoke("Cooldown", cooldownTime);
            multi = 1;
            Debug.Log($"{hits % 3}");
        }
    }

    void Cooldown()
    {
        cooldown = !cooldown;
    }

    void ChangeMat()
    {
        MR.material = mats[1];
        Debug.Log("worksDown");
    }

    void ChangeMatBack()
    {
        MR.material = mats[0];
    }
}
