using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] chests;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private TMP_Text textBring;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private int maxV = 8;
    public static bool chestTaken = false;
    private GameObject chest;
    private string chestKind;
    private int goldEarned;
    private void Start()
    {
        int rnd = Random.Range(0, 3);
        chest = Instantiate(chests[rnd], this.transform);
        chest.GetComponent<Rigidbody>().isKinematic = true;
        chestKind = chest.tag;
        chestTaken = false;
        switch (rnd)
        {
            case 0:
                goldEarned = Random.Range(5, 10);
                break;
            case 1:
                goldEarned = Random.Range(9, 14);
                break;
            case 2:
                goldEarned = Random.Range(12, 20);
                break;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!chestTaken)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    slider.value += 1;
                    if (slider.value == maxV)
                    {
                        slider.gameObject.SetActive(false);
                        text.gameObject.SetActive(false);
                        slider.value = 0;
                        Destroy(this.gameObject);
                        FindObjectOfType<GameManager>().ChestCollected(goldEarned, chestKind);
                        chestTaken = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            slider.value = 0;
            slider.maxValue = maxV;
            if (!chestTaken)
            {
                slider.gameObject.SetActive(true);
                text.gameObject.SetActive(true);
            }
            else
            {
                textBring.gameObject.SetActive(true);
            }
           
                
                
                //chests = gm.CHESTScollected;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            slider.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            textBring.gameObject.SetActive(false);
        }
    }

    
}
