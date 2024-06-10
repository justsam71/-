using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChessAnomalies : Anomaly
{
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Camera cm;
    [SerializeField]
    private AnomalyManager am;
    [SerializeField]
    private GameObject finalLights;
    [SerializeField]
    private VolumeProfile finalVolume;
    private int rnd;
    private float tmp;

    [SerializeField]
    private GameObject coins;
    [SerializeField]
    private GameObject LevelGenerator;

    [Header("Anomaly Chances")]
    [SerializeField]
    private int panelRotationChance = 33;
    [SerializeField]
    private int skyboxRotationChance = 33;
    [SerializeField]
    private int oneTypePlatformChance = 33;
    [SerializeField]
    private int coinChance = 1;

    public override void StartAnomaly()
    {
        rnd = UnityEngine.Random.Range(0, 101);

        if (rnd < panelRotationChance)
        {
            firstAnomaly();
            am.Anounce("Platform Rotation", 10);
        }
        else if (rnd < panelRotationChance + skyboxRotationChance)
        {
            secondAnomaly();
            am.Anounce("Dizzzzy", 14);
        }
        else if (rnd < panelRotationChance + skyboxRotationChance + oneTypePlatformChance)
        {
            thirdAnomaly();
            am.Anounce("Only One Type", 14);
        }
        else if (rnd < panelRotationChance + skyboxRotationChance + oneTypePlatformChance + coinChance)
        {
            fourthAnomaly();
            am.Anounce("Coins, coins, coins", 14);
        }
    }

    public override void firstAnomaly()
    {
        foreach (GameObject panel in GameObject.FindGameObjectsWithTag("Panel"))
        {
            panel.GetComponent<ChessPanel>().ChangeMaterialAnomaly();
        }
    }

    /*
     * ƒл€ оптимизации лучше переделать и не использовать метод FindGameObjectWithTag(). ¬место этого можно заранее сохран€ть 
     * объекты в экземпл€ре GameObject.
     */
    public override  void secondAnomaly()
    {
        tmp = FindObjectOfType<GameManager>().skyboxRotationSpeed;
        FindObjectOfType<GameManager>().skyboxRotationSpeed = 200;
        GameObject.FindGameObjectWithTag("Tower").GetComponent<Rotate>().y = 0.12f; 
        Invoke("SkyboxRotationBack", 15);
    }
    void SkyboxRotationBack()
    {
        FindObjectOfType<GameManager>().skyboxRotationSpeed = tmp;
        if (GameObject.FindGameObjectWithTag("Tower") != null)
            GameObject.FindGameObjectWithTag("Tower").GetComponent<Rotate>().y = 0;

    }

    public override void thirdAnomaly()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Area"))
            Destroy(go, 0.1f);
        LevelGenerator.GetComponent<LevelGenerator2>().isFirst = true;
        LevelGenerator.GetComponent<LevelGenerator2>().OneTypeAnomaly();
        Invoke("NewLevel", 14.5f);
    }

    void NewLevel()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Area"))
            Destroy(go, 0.1f);
        LevelGenerator.GetComponent<LevelGenerator2>().isFirst = true;
        LevelGenerator.GetComponent<LevelGenerator2>().OneTypeAnomaly();
    }

    public override void fourthAnomaly()
    {
        Instantiate(coins);
    }

    public override void fifthAnomaly()
    {
        Instantiate(coins);
    }

    public override void FinalAnomaly()
    {
        finalLights.SetActive(true);
        am.Anounce("FINAL ANOMALY TIME!!!", 15);
        am.anouncement.color = Color.red;
        cm.GetComponent<Volume>().profile = finalVolume;
        Invoke("End", am.finalAnomalyTime - 0.5f);
        am.changeMusic = true;
    }

    public override void End()
    {
        am.finale = true;
        gm.EndGame();
    }
}
