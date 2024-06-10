using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestBackTrigger : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private int maxV = 8;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ChestTrigger.chestTaken == true)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    slider.value += 1;
                    if (slider.value == maxV)
                    {
                        slider.gameObject.SetActive(false);
                        text.gameObject.SetActive(false);
                        slider.value = 0;
                        FindObjectOfType<GameManager>().ChestCarried();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gm.carryingChest == true)
        {
            slider.value = 0;
            slider.maxValue = maxV;
            slider.gameObject.SetActive(true);
            text.gameObject.SetActive(true);

        }
    }
}
