using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{

    private Transform target;
    [SerializeField]
    private Transform fixedAim;
    [SerializeField]
    private Transform rocketParentCenter;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float ascendingSpeed;
    [SerializeField]
    private float launchSpeed;

    private bool launch = false;
    private Vector3 direction;

    [SerializeField]
    private int explosionRadius;
    [SerializeField]
    private int explosionForce;
    [SerializeField]
    private GameObject explosionParticle;
    [SerializeField]
    private GameObject fire;

    private void Start()
    {
        Invoke("Explode", 14);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rocketParentCenter.position = new Vector3(0, Random.Range(0, 100), 0);
        rocketParentCenter.Rotate(new Vector3(0, Random.Range(0, 360), 0));
    }

    private void FixedUpdate()
    {
        if (launch == false)
        {
            rocketParentCenter.Rotate(new Vector3(0, -rotationSpeed, 0));
            if (rocketParentCenter.position.y < target.position.y - 1)
                rocketParentCenter.position = new Vector3(0, rocketParentCenter.position.y + ascendingSpeed, 0);
            else if (rocketParentCenter.position.y > target.position.y + 1)
                rocketParentCenter.position = new Vector3(0, rocketParentCenter.position.y - ascendingSpeed, 0);
        }
        else
        {
            direction = (fixedAim.transform.position - this.transform.position).normalized;
            this.transform.Translate(direction * launchSpeed * Time.deltaTime, Space.World);
        }

        if (launch == false && Vector3.Distance(this.transform.position, target.position) < 7)
        {
            fixedAim.position = target.position;
            this.transform.LookAt(fixedAim);
            fixedAim.SetParent(this.transform);
            launch = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false ;
    }

    void Explode()
    {
        if (this.gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            explosionParticle.SetActive(true);
        }
        Destroy(fire);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Применяем силу взрыва ко всем объектам в радиусе
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Debug.Log("explosion");
            }
        }
        Destroy(rocketParentCenter.gameObject, 1f);
    }
}
