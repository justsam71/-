using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator2 : MonoBehaviour
{
    public float maxY;
    private float currentY;
    private float currentRotation;
    public GameObject[] platforms;

    private int lastPlatform;
    public GameObject FinalPlatform;
    [SerializeField]
    private int maxTowerHeight;
    [SerializeField]
    private Material[] finalPlatformMaterials;
    public Material finalPlatformMaterial;
    private GameObject platform;
    public Transform Parent;
    public bool isFirst = true;
    private float generationSpeed = 0f;
    private int rnd;

    private bool OneTypeOnly = false;
    private int oneType;

    void Start()
    {
        LevelGenerate();
        finalPlatformMaterial = finalPlatformMaterials[Random.Range(0, finalPlatformMaterials.Length)];
    }

    public void LevelGenerate()
    {
        if (isFirst)
        {
            Reroll();
            currentY = platforms[rnd].transform.position.y;
            currentRotation = platforms[rnd].transform.eulerAngles.y;
            platform = Instantiate(platforms[rnd]);
            lastPlatform = rnd;
            isFirst = false;

            platform.transform.SetParent(Parent);
        }
        Reroll();




        if (currentY < maxY)
        {
            CalculatePosition();
            platform = Instantiate(platforms[rnd]);
            lastPlatform = rnd;
            platform.transform.position = new Vector3(0, currentY, 0);
            platform.transform.eulerAngles = new Vector3(0, currentRotation, 0);
            platform.transform.SetParent(Parent);

            Invoke("LevelGenerate", generationSpeed);
        }
        else
        {
            generationSpeed = 0.4f; // для аномалии
            BuildFinal();
        }


    }

    void CalculatePosition()
    {
        List<List<float>> heightChange = new List<List<float>>();
        List<List<float>> rotationChange = new List<List<float>>();

        if (SceneManager.GetActiveScene().buildIndex == 1) //для первого уровня
        {
            heightChange = new List<List<float>>() {
            new List<float> { 4.5f, 4.5f, 4.5f, -0.5f, -0.5f }, // [1][x] :  1 столбик значит идёт первая платформа, а затем [x]
            new List<float> { 4.5f, 4.5f, 4.5f, -0.5f, -0.5f },
            new List<float> { 4.5f, 4.5f, 4.5f, -0.5f, -0.5f },
            new List<float> { 15.6f, 15.6f, 15.6f, 10.2f, 9.5f },
            new List<float> { 12.4f, 12.4f, 12.4f, 7.5f, 7f }
        };
            rotationChange = new List<List<float>>() {
            new List<float> { 136.5f, 136.5f, 122f, 145f, 143f },
            new List<float> { 136.5f, 136.5f, 122f, 145f, 143f },
            new List<float> { 143f, 143f, 135f, 152f, 148f },
            new List<float> { 128.5f, 128.5f, 123f, 136.5f, 136.5f },
            new List<float> { 40f, 40f, 40f, 45f, 50f }
        };
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) {  //для второго
            heightChange = new List<List<float>>() {  //
            new List<float> { 4.4f, 4.4f, 5f, 2.5f},
            new List<float> { 4.4f, 4.4f, 5f, 2.5f},
            new List<float> { 3f, 3f, 4f, 1.5f},
            new List<float> { 3.2f, 3.2f, 3.5f, 1f}
        };
            rotationChange = new List<List<float>>() {
            new List<float> { 140.5f, 140.5f, 160f, 140.5f},
            new List<float> { 140.5f, 140.5f, 180f, 140.5f },
            new List<float> { 75f, 75f, 85f, 75f },
            new List<float> { 75f, 75f, 85f, 85f}
        };

        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            heightChange = new List<List<float>>() {
            new List<float> { 11f, 10.5f, 12f},
            new List<float> { 6f, 6f, 6f},
            new List<float> { 14f, 14f, 16.5f}
        };
            rotationChange = new List<List<float>>() {
            new List<float> { 125f, 125f, 50f},
            new List<float> { 130f, 125f, 50f},
            new List<float> { 160f, 160f, 95f}
        };
        }

        currentY += heightChange[lastPlatform][rnd];
        currentRotation -= rotationChange[lastPlatform][rnd];
    }
    void Reroll()
    {
        rnd = UnityEngine.Random.Range(0, platforms.Length);

        if (rnd == 3 && lastPlatform == 3 && SceneManager.GetActiveScene().buildIndex == 2)  //2 льда не могут быть вместе т.к. это слишком низко для уровня
        {
            Reroll();
        }

        if (rnd == 3 && currentY + 8 > maxY)  //3я платформа не может быть последней
        {
            Reroll();
        }
        if (rnd == 4 && currentY + 8 > maxY && OneTypeOnly == false)  //3я платформа не может быть последней
        {
            Reroll();
        }

        if (OneTypeOnly == true)
        {
            rnd = oneType;
        }
    }

    void BuildFinal()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) //1level
        {
            if (lastPlatform == 0 || lastPlatform == 1 || lastPlatform == 2)
            {
                currentRotation -= 145f;
                currentY -= 0.5f;
            }
            else if (lastPlatform == 3)
            {
                currentRotation -= 133f;
                currentY += 9.5f;
            }
            else if (lastPlatform == 4)
            {
                currentRotation -= 45f;
                currentY += 6.5f;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) //2level
        {
            if (lastPlatform == 0 || lastPlatform == 1)
            {
                currentRotation += 50f;
                currentY += 22.8f;
            }

            if (lastPlatform == 2 || lastPlatform == 3)
            {
                currentRotation += 120f;
                currentY += 21f;
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 3) //2level
        {
            if (lastPlatform == 0 )
            {
                currentRotation += 135f;
                currentY += 23f;
            }

            if (lastPlatform == 1)
            {
                currentRotation += 135f;
                currentY += 18.5f;
            }
            if (lastPlatform == 2)
            {
                currentRotation += 100f;
                currentY += 26.5f;
            }
        }

        while (currentY < maxTowerHeight)
        {

            platform = Instantiate(FinalPlatform);
            platform.transform.position = new Vector3(0, currentY, 0);
            platform.transform.eulerAngles = new Vector3(0, currentRotation, 0);
            currentRotation -= 10;
            platform.transform.SetParent(Parent);

            currentY += 0.95f;
        }
    }   

    public void OneTypeAnomaly()
    {
        oneType = 3;
        OneTypeOnly = !OneTypeOnly;
        while (oneType == 3)
            oneType = UnityEngine.Random.Range(0, platforms.Length);
        LevelGenerate();
    }
}
