using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScroll : MonoBehaviour
{
    [SerializeField]
    private Transform characterRotator;
    [SerializeField]
    private string direction;
    private float rotationSpeed;
    private float sum = 0;
    private bool startRotate = false;
    private static int clickCount = 0;
    private void OnMouseDown()
    {
        if (!startRotate)
        {
            switch (direction)
            {
                case "left":
                    rotationSpeed = -0.5f;
                    startRotate = true;
                    break;
                case "right":
                    rotationSpeed = 0.5f;
                    startRotate = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        if (startRotate)
        {
            if (sum < 34 && sum > -34 ) //34 - расстояние между персонажами
            {
                sum += rotationSpeed;
                characterRotator.Rotate(new Vector3(0, 0, rotationSpeed));
            }
            else
            {
                sum = 0;
                startRotate = false;
                if (rotationSpeed < 0) //крутится в правую сторону
                {
                    clickCount += 1;
                }
                else if (rotationSpeed > 0) // крутится в левую
                {
                    clickCount -= 1;
                }

            }

        }

        if (clickCount == -1 || clickCount == 3)
        {
            SmoothRotation();
        }


    }

    void SmoothRotation()
    {
        if (clickCount == -1)
        {
            characterRotator.Rotate(0, 0, -102); // 34 * 3 ( количество персонажей * расстояние между ними)
            clickCount = 2;
        }
        if (clickCount == 3)
        {
            characterRotator.Rotate(0, 0, 102);
            clickCount = 0;
        }
        Debug.Log(clickCount);
    }
}
