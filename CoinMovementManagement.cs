using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovementManagement : MonoBehaviour
{
    private float z = 0;
    private float y = 0;
    private float x = 0;
    [SerializeField]
    private float coinSpeed;
    Vector3 targetVector = new Vector3(0,0,0);


    void Start()
    {
        RandomPoint();
        this.transform.position = new Vector3(x, y, z);
    }


    void Update()
    {
        if (this.transform.position == targetVector)
        {
            RandomPoint();
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(x, y, z), coinSpeed * Time.deltaTime);
        }
    }

    void RandomPoint()
    {
        z = Random.Range(-20, 20);
        x = Random.Range(-20, 20);
        y = Random.Range(-8, 68);
        targetVector = new Vector3(x, y, z);
    }
}
