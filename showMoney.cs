using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showMoney : MonoBehaviour
{   
    [SerializeField]
    private Animator animator;
    private void OnMouseEnter()
    {
        animator.SetBool("open", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("open", false);
        animator.SetTrigger("closeTrigger");
    }
}
