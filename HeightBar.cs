using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightBar : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float SpawnCoordinatesY = 7.3f;
    [SerializeField]
    private float FinishCoordinatesY = 73f;
    [SerializeField]
    private Scrollbar scrollbar;

    private void Update()
    {
        if (Player.transform.position.y < SpawnCoordinatesY)
            scrollbar.value = 0;
        else if (Player.transform.position.y > FinishCoordinatesY)
            scrollbar.value = 1;
        else
        {
            scrollbar.value = (Player.transform.position.y - SpawnCoordinatesY ) / (FinishCoordinatesY - SpawnCoordinatesY);
        }
    }
   
}
