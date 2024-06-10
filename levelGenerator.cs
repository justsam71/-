using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
    public float maxY;
    private float currentY;
    private float currentRotation;
    public GameObject[] platforms;
    private int lastPlatform;
    public GameObject FinalPlatform;
    private GameObject platform;
    public bool isFirst = true;
    private float heightDifference = 0;
    private float rotationDifference = 0;
    private float generationSpeed = 0.2f;
    private int rnd;


    private bool OneTypeOnly = false;
    private int oneType;

    void Start()
    {
        LevelGenerate();
    }
    public void LevelGenerate()
    {
        Reroll();
        platform = Instantiate(platforms[rnd]);
        lastPlatform = rnd;

        if (isFirst)
        {
            currentY = platforms[rnd].transform.position.y;
            currentRotation = platforms[rnd].transform.eulerAngles.y;
            isFirst = false;
        }


        switch (rnd)
        {
            case 0: //area 1
                heightDifference = 0;  //отступ с начала?
                rotationDifference = 0;  //отступ с начала?
                break;
            case 1: //area 2
                heightDifference = 0;
                rotationDifference = 0;
                break;
            case 2: //area 3
                heightDifference = 0;
                rotationDifference = -10;
                break;
            case 3: //area 4
                heightDifference = 5.15f;
                rotationDifference = 5;
                break;
            case 4: //area 5
                heightDifference = 4.5f;
                rotationDifference = 10f;
                break;
                    
        }

        platform.transform.position = new Vector3(0, currentY - heightDifference, 0);
        platform.transform.eulerAngles = new Vector3(0, currentRotation - rotationDifference, 0);

        switch (rnd) //для следующего
        {
            case 0: //area 1
                currentY += 4.75f + heightDifference;  //слева высота, справа отступ с начала
                currentRotation -= 136.5f + rotationDifference; //слева высота, справа отступ с начала
                break;
            case 1: //area 2
                currentY += 4.75f + heightDifference;
                currentRotation -= 136.5f + rotationDifference;
                break;
            case 2: //area 3
                currentY += 4.75f + heightDifference;
                currentRotation -= 136.5f + rotationDifference;
                break;
            case 3: //area 4
                currentY += 5f + heightDifference;
                currentRotation -= 136.5f + rotationDifference;
                break;
            case 4: //area 5
                currentY += 3f + heightDifference;
                currentRotation -= 40f + rotationDifference;  
                break;

        }

        if (currentY < maxY)
        {
            Invoke("LevelGenerate", generationSpeed);
        }
        else
        {
            generationSpeed = 0.4f; // для аномалии
            BuildFinal();
        }
    }
    void Reroll()
    {
        rnd = UnityEngine.Random.Range(0, platforms.Length);
        if (rnd == 3 && isFirst) // платформа не первого типа не может быть первой
        {
            Reroll();
        }
        if (rnd == 4 && isFirst) // платформа не первого типа не может быть первой
        {
            Reroll();
        }
        //if (rnd == 1 && lastPlatform == 1) //чтобы пробел не повторялся
        //{
        //    Reroll();
        //}

        if (OneTypeOnly == true)
        {
            rnd = oneType;
        }
    }

    void BuildFinal()
    {
        while (currentY <= 68)
        {
            if (lastPlatform == 0 || lastPlatform == 1 || lastPlatform == 2)
            {
                heightDifference = 6.5f;
            }
            if (lastPlatform != -1)
                currentRotation -= 5;
            if (lastPlatform == 4)
                currentRotation -= 10;
            platform = Instantiate(FinalPlatform);
            platform.transform.position = new Vector3(0, currentY - heightDifference, 0);
            platform.transform.eulerAngles = new Vector3(0, currentRotation + rotationDifference, 0);
            currentRotation -= 10;

            currentY += 1;
            lastPlatform = -1;
        }
    }

    public void OneTypeAnomaly()
    {
        OneTypeOnly = !OneTypeOnly;
        oneType = UnityEngine.Random.Range(0, platforms.Length);
        LevelGenerate();
    }
}
