using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleWay : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform rotateTarget;
    [SerializeField]
    private float offsetY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0, player.position.y, 0);
        this.transform.rotation = Quaternion.Euler(-90, rotateTarget.eulerAngles.y - offsetY, 0);
    }
}
