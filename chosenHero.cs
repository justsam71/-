using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chosenHero : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hero;

    [SerializeField]
    private GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "panda")
        {
            hero.text = "PANDA";
        }
        else if (other.tag == "fox")
        {
            hero.text = "FOX";
        }
        else if (other.tag == "spider")
        {
            hero.text = "SPIDER";
        }

        gm.CharacterChoice(other.tag);
    }
}
