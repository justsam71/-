using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVector : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float y;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.position;

        Quaternion targetRotation = target.rotation;

        Quaternion newRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - y, 0);

        transform.rotation = newRotation;

    }
}
