using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingMenu : MonoBehaviour
{
    private bool movingRight = false;
    private bool movingLeft = false;
    [SerializeField]
    public RectTransform myRectTransform;
    [SerializeField]
    public GameObject buttonBack;
    private int placement = 0; // -1 = left; 0 = middle; 1 = right


    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            if (placement == -1)
                if (myRectTransform.localPosition.x < 650)
                {
                    myRectTransform.localPosition += 10 * Vector3.right;
                    buttonBack.SetActive(false);
                }
            if (placement == 0)
                if (myRectTransform.localPosition.x < 0)
                {
                    myRectTransform.localPosition += 10 * Vector3.right;
                    buttonBack.SetActive(true);
                }
        }
        if (movingLeft)
        {
            if (placement == 0)
                if (myRectTransform.localPosition.x > 0)
                {
                    myRectTransform.localPosition += 10 * Vector3.left;
                    buttonBack.SetActive(true);
                }
            if (placement == 1)
                if (myRectTransform.localPosition.x > -650)
                {
                    myRectTransform.localPosition += 10 * Vector3.left;
                    buttonBack.SetActive(false);

                }
        }
    }

    public void LeftButton() // вызов у левой кнопки
    {
        if (placement == 0)
        {
            movingRight = true;
            placement = -1;
            movingLeft = !movingRight;
        }
        else if (placement == -1)
        {
            movingLeft = true;
            placement = 0;
            movingRight = !movingLeft;
        }
    }

    public void RightButton() // вызов у правой кнопки
    {
        if (placement == 0)
        {
            movingLeft = true;
            placement = 1;
            movingRight = !movingLeft;
        }
        else if (placement == 1)
        {
            movingRight = true;
            placement = 0;
            movingLeft = !movingRight;
        }
    }

}
