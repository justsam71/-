using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantRotation : MonoBehaviour
{

    void FixedUpdate()
    {
        this.transform.Rotate(0, -0.025f, 0);
    }
}
