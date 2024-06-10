using UnityEngine;
using UnityEngine.Rendering;

public class SnowAnomalies : Anomaly
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
    private GameObject directionalLight;
    [SerializeField]
    private Rigidbody playerRB;
    [SerializeField]
    private Transform blizzardDirection;
    [SerializeField]
    private ParticleSystem blizzardSnow;
    [SerializeField]
    private ParticleSystem wind;
    [SerializeField]
    private float blizzardForce;
    private bool blizzard;
    [SerializeField]
    private Material[] iceSnowMat;
    static public bool ice = false;

    [Header("Anomaly Chances")]
    [SerializeField]
    private int blizzardChance = 33;
    [SerializeField]
    private int iceChance = 33;
    [SerializeField]
    private int coldChance = 33;
    [SerializeField]
    private int coinChance = 1;

    private void FixedUpdate()
    {
        if (blizzard)
        {
            Vector3 circlewayBackwardVector = (blizzardDirection.position - playerRB.gameObject.transform.position);

            playerRB.AddForce(circlewayBackwardVector.normalized * blizzardForce * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
    public override void StartAnomaly()
    {
        rnd = UnityEngine.Random.Range(0, 101);

        if (rnd < blizzardChance)
        {
            firstAnomaly();
            am.Anounce("Blizzard", 10);
        }
        else if (rnd < blizzardChance + iceChance)
        {
            secondAnomaly();
            am.Anounce("Ice", 14);
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
        directionalLight.SetActive(false);
        RenderSettings.fog = true;
        Invoke("BlizzardFinish", 10);
        blizzard = true;
        var emmisionModule = blizzardSnow.emission;
        emmisionModule.rateOverTime = 50;
        var emmisionModule2 = wind.emission;
        emmisionModule2.rateOverTime = 100;
    }

    void BlizzardFinish()
    {
        directionalLight.SetActive(true);
        RenderSettings.fog = false;
        blizzard = false;
        var emmisionModule = blizzardSnow.emission;
        emmisionModule.rateOverTime = 0;
        var emmisionModule2 = wind.emission;
        emmisionModule2.rateOverTime = 0;
    }

    public override void secondAnomaly()
    {
        foreach (GameObject snowyPanel in GameObject.FindGameObjectsWithTag("snowyPanel"))
        {
            snowyPanel.GetComponent<MeshRenderer>().material = iceSnowMat[0];
        }
        Invoke("iceFinish", 14);
        ice = true;
    }

    void iceFinish()
    {
        foreach (GameObject snowyPanel in GameObject.FindGameObjectsWithTag("snowyPanel"))
        {
            snowyPanel.GetComponent<MeshRenderer>().material = iceSnowMat[1];
        }
        ice = false;
    }

    public override void thirdAnomaly()
    {

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
