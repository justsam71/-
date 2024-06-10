using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2 : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Vector3 OffsetOnTheTop;
    public Vector3 OffsetEndGame;

    public float speed = 1;
    bool topTriggered = false;

    public bool atTheStart = false;
    public bool gameEnd = false;
    [SerializeField]
    private Transform startPosition;



    void Update()
    {
        topTriggered = FindObjectOfType<GameManager>().ReachedTheTop;
        if (gameEnd)
        {
            transform.position = Vector3.Lerp(transform.position, OffsetEndGame,  speed * Time.deltaTime);
        }
        else if (atTheStart)
        {
            transform.position = new Vector3(startPosition.position.x, startPosition.position.y, startPosition.position.z );
        }
        else if (topTriggered == false)
        {
            offset = new Vector3(player.position.x, 1.5f, player.position.z);

            transform.position = player.position + offset;
            //Debug.Log(player.position);
        }
        else if (topTriggered == true)  
        {
            transform.position = Vector3.Lerp(transform.position, OffsetOnTheTop, speed * Time.deltaTime);
        }
        
    }
}