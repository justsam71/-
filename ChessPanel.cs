using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPanel : MonoBehaviour
{
    [SerializeField]
    private Material white;
    [SerializeField]
    private Material black;
    [SerializeField]
    private Material purple;
    public int currentMaterial;
    [SerializeField]
    private int percentage;
    [SerializeField]
    private float XrotationSpeed = 3f;
    [SerializeField]
    private float YrotationSpeed = 0;
    private float knockbackForce= 150f;

    private Vector3 initRotation;

    private void Start()
    {
        initRotation = this.transform.eulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int rnd = Random.Range(0, 100);

        if(collision.gameObject.tag == "Player")
        {
            if (rnd > percentage)
            {
                if (currentMaterial == 1)  //if white
                {
                    this.GetComponent<MeshRenderer>().material = black;
                    currentMaterial = 2;
                }
                else if (currentMaterial == 2) // if black
                {
                    this.GetComponent<MeshRenderer>().material = white;
                    currentMaterial = 1;
                }
            }
            else
            {
                if (currentMaterial != 3)
                {
                    this.GetComponent<MeshRenderer>().material = purple;
                    currentMaterial = 3;
                }
            }
            if (currentMaterial == 3)
            {
                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    //Vector3 knockbackDirection = collision.contacts[0].normal;
                    Vector3 knockbackDirection = (new Vector3(0, collision.gameObject.transform.position.y + 1, 0) - collision.transform.position).normalized;

                    playerRigidbody.AddForce(-knockbackDirection * knockbackForce, ForceMode.Impulse);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentMaterial == 3)
        {
            this.gameObject.transform.Rotate(XrotationSpeed, 0, YrotationSpeed, Space.Self);
        }
    }

    public void ChangeMaterialAnomaly()
    {
        this.GetComponent<MeshRenderer>().material = purple;
        currentMaterial = 3;
        Invoke("stopRotation", 10);
    }

    void stopRotation()
    {
        this.transform.eulerAngles = initRotation;
        int rnd = Random.Range(1, 3);
        if (rnd == 1)
        {
            currentMaterial = 1;
            this.GetComponent<MeshRenderer>().material = white;
        }
        else if (rnd == 2)
        {
            currentMaterial = 2;
            this.GetComponent<MeshRenderer>().material = black;
        }
    }
}
