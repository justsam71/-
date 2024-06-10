using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWithChance : MonoBehaviour
{
    [SerializeField]
    private int percentage;
    [SerializeField]
    private int maxHeight;
    private int rnd;

    void Start()
    {
        rnd = Random.Range(0, 100);
        if (rnd > percentage || this.GetComponentInParent<Transform>().position.y > maxHeight) 
            this.gameObject.SetActive(false);
    }

}
