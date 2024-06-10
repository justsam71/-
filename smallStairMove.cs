using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallStairMove : MonoBehaviour
{
    private bool left = false;
    private bool gradualMoves = false;
    private int rnd;


    private float speed = 0.04f;
    private float moveAmount = 0;
    void Start()
    {
        rnd = Random.Range(1,3);

        if (rnd == 1)
        {
            suddenMoves();
        }

        if (rnd == 2)
        {

            gradualMoves = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gradualMoves)
        {
            if (left)
            {
                this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speed, transform.eulerAngles.z);
                moveAmount += speed;
                if (moveAmount >= 7)
                    left = !left;
            }
            if (!left)
            {
                this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - speed, transform.eulerAngles.z);
                moveAmount -= speed;
                if (moveAmount <= 0)
                    left = !left;
            }
        }
    }

    void suddenMoves()
    {
        if (left)
            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 7, transform.eulerAngles.z);
        else
            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 7, transform.eulerAngles.z);
        left = !left;

        Invoke("suddenMoves", 2);
    }
}
