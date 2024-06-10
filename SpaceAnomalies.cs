using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpaceAnomalies : Anomaly
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
    private Rigidbody playerRB;
    [SerializeField]
    private GameObject rocket;



    [Header("Anomaly Chances")]
    [SerializeField]
    private int strangeGravityChance = 33;
    [SerializeField]
    private int rocketLaunchChance = 33;
    [SerializeField]
    private int oneTypePlatformChance = 33;
    [SerializeField]
    private int coinChance = 1;

    public override void StartAnomaly()
    {
        rnd = UnityEngine.Random.Range(0, 101);

        if (rnd < strangeGravityChance)
        {
            firstAnomaly();
            am.Anounce("Gravitational Shifts", 10);
        }
        else if (rnd < strangeGravityChance + rocketLaunchChance)
        {
            secondAnomaly();
            am.Anounce("Rocket Launch", 14);
        }
        //else if (rnd < panelRotationChance + skyboxRotationChance + oneTypePlatformChance)
        //{
        //    thirdAnomaly();
        //    am.Anounce("Only One Type", 14);
        //}
        //else if (rnd < panelRotationChance + skyboxRotationChance + oneTypePlatformChance + coinChance)
        //{
        //    fourthAnomaly();
        //    am.Anounce("Coins, coins, coins", 14);
        //}
    }

    public override void firstAnomaly()
    {
        playerRB.useGravity = false;
        Invoke("gravityFalse", 10);
    }

    private void gravityFalse()
    {
        playerRB.useGravity = true;
    }

    /*
     * ƒл€ оптимизации лучше переделать и не использовать метод FindGameObjectWithTag(). ¬место этого можно заранее сохран€ть 
     * объекты в экземпл€ре GameObject.
     */
    public override void secondAnomaly()
    {
        Instantiate(rocket);
        Instantiate(rocket);
        Instantiate(rocket);
    }

    public override void thirdAnomaly()
    {
        Instantiate(rocket);
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
