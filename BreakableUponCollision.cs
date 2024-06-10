using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableUponCollision : MonoBehaviour
{
    [SerializeField]
    private int hits;
    private int hitsMax;
    [SerializeField]
    private int timeToAppearAgain;
    [SerializeField]
    private int explosionBurst;
    [SerializeField]
    private GameObject fracturedObject;
    [SerializeField]
    private GameObject parentLevel;
    private void Start()
    {
        hitsMax = hits;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hits--;
            Debug.Log($"{hits}");
            if (hits == 0)
            {
                if (fracturedObject != null)
                {
                    GameObject fracturedSnowBlock = Instantiate(fracturedObject, parentLevel.transform.position, parentLevel.transform.rotation);
                    Rigidbody[] allRigidBodies = fracturedObject.GetComponentsInChildren<Rigidbody>();
                    if (allRigidBodies.Length > 0)
                    {
                        foreach(Rigidbody body in allRigidBodies)
                        {
                            body.AddExplosionForce(explosionBurst, transform.position, 0.1f);
                        }
                    }

                    Destroy(fracturedSnowBlock, 3);
                }
                Break();
            }
        }
    }

    public void Break()
    {
        this.gameObject.SetActive(false);
        if (timeToAppearAgain > 0)  //если отрицательно, то объект не должен заново появляться
        {
            Invoke("AppearAgain", timeToAppearAgain);
        }
    }
    void AppearAgain()
    {
        this.gameObject.SetActive(true);
        hits = hitsMax;
    }
}
