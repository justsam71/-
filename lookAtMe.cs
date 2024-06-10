using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtMe : MonoBehaviour
{
    public Transform player;
    private bool topTriggered;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private float endY;
    public bool gameEnd = false;

    void Update()
    {
        topTriggered = FindObjectOfType<GameManager>().ReachedTheTop;
        if (gameEnd)
        {
            transform.position = new Vector3(0, endY, 0);
        }
        else if (!topTriggered)
        {
            transform.position = new Vector3(0, player.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(player.position.x / 2, player.position.y, player.position.z / 2);
        }
    }

}
