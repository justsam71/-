using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chestOpenHover : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinAmount;

    [SerializeField]
    private Animator anim;
    private void OnMouseEnter()
    {
        anim.Play("chesOpen");
        coinAmount.text = $"{MainMenu.dts.coins}";
    }

    private void OnMouseExit()
    {
        anim.Play("idleChest");
    }
}
