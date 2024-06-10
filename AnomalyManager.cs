using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class AnomalyManager : MonoBehaviour
{
    [SerializeField]
    private Anomaly anomalies;
    
    private float timer;
    private float gameTime;
    private float tmpFlash;
    private float flashCooldown = 0.5f;
    private float amountOfPlayers = 1;

    public TMP_Text firstMinute;
    public TMP_Text secondMinute;
    public TMP_Text separator;
    public TMP_Text firstSecond;
    public TMP_Text secondSecond;
    private float seconds;

    public TMP_Text anouncement;

    [SerializeField]
    private Animator anomalyTextAnimation;

    [SerializeField]
    private TMP_Text finalAnomalyChanceTXT;
    public float finalAnomalyChance = 0; //0
    private int finalRND = 999;
    public bool finale = false;



    [SerializeField]
    private AudioSource scr;
    bool playOnce = true;
    [SerializeField]
    private AudioSource music;
    public bool changeMusic = false;

    [Header("Difficulty Settings")]
    private int timeBeforeFirst = 15;
    private int anomalyCooldown = 15;
    private int SafeZoneTime = 61;
    private int makeTextRed = 59;
    private int anomalyPercentageIncrease = 3;
    public int finalAnomalyTime = 15;


    private void Start()
    {
        SetAnomalyDifficulty();
        timer = timeBeforeFirst;
    }
    void Update()
    {
        if (changeMusic)
        {
            if (music.pitch > 0.1)
            {
                music.pitch -= 0.01f;
                music.volume += 0.01f;
            }
        }

        if (timer < 3)
        {
            if (playOnce)
            {
                scr.Play();
                Debug.Log("dssadfasf");
                playOnce = false;
            }
        }
        else
        {
            playOnce = true;
        }


        if (!finale)
        {
            timer -= Time.deltaTime;
            gameTime += Time.deltaTime;
        }

        if (gameTime >= makeTextRed)
        {
            finalAnomalyChanceTXT.color = Color.red;
        }

        if (timer <= 0 & !finale)
        {
            if (gameTime >= SafeZoneTime & finalAnomalyChance < finalRND) 
            {
                finalRND = UnityEngine.Random.Range(0, 101);
            }
            
            if (finalAnomalyChance >= finalRND)
            {
                timer = finalAnomalyTime;
                tmpFlash = finalAnomalyTime;
                anomalies.FinalAnomaly();
            }
            else //срабатывает обычная аномалия
            {
                finalAnomalyChance += anomalyPercentageIncrease;//3
                finalAnomalyChanceTXT.text = $"{finalAnomalyChance} %";
                timer = anomalyCooldown;
                anomalies.StartAnomaly();
                SetTextDisplay(true);
            }
        }
        if (timer < 5.4f || finalAnomalyChance > finalRND - anomalyPercentageIncrease)//3
        {
            firstMinute.color = Color.red;
            secondMinute.color= Color.red;
            separator.color= Color.red;
            firstSecond.color = Color.red;
            secondSecond.color = Color.red;
            Flash();
        }
        else 
        {
            tmpFlash = 5f;
            firstMinute.color = Color.white;
            secondMinute.color = Color.white;
            separator.color = Color.white;
            firstSecond.color = Color.white;
            secondSecond.color = Color.white;
        }


        if (!finale)
            UpdateTimerDisplay();
    }

    public void Anounce(string anomalyName, float time)
    {
        anouncement.text = anomalyName;
        anouncement.rectTransform.position = new Vector3(anouncement.rectTransform.position.x, 0, anouncement.rectTransform.position.y);
        anomalyTextAnimation.SetTrigger("text");
        Invoke("AnounceClear", time);
    }
    void AnounceClear()
    {
        anouncement.text = "";
    }

    public void PlayerDied()
    {
        finalAnomalyChance += 5 / amountOfPlayers;
        finalAnomalyChanceTXT.text = $"{finalAnomalyChance} %";
    }

    private void UpdateTimerDisplay()
    {

        seconds = Mathf.FloorToInt(timer % 60);
        string currentTime = string.Format("{00:00}{1:00}", 0, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if (timer <= tmpFlash)
        {
            SetTextDisplay(!firstMinute.IsActive());
            tmpFlash = timer - flashCooldown;
        }
    }
    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
    }

    void SetAnomalyDifficulty()
    {
       switch ((int) MainMenu.chosenDifficulty)
        {
            case 0: //Easy
                timeBeforeFirst = 15;
                anomalyCooldown = 25;
                SafeZoneTime = 151;
                makeTextRed = 149;
                anomalyPercentageIncrease = 1;
                finalAnomalyTime = 20;
                break;
            case 1: //Medium
                timeBeforeFirst = 15;
                anomalyCooldown = 20;
                SafeZoneTime = 101;
                makeTextRed = 99;
                anomalyPercentageIncrease = 2;
                finalAnomalyTime = 15;
                break;
            case 2: //Hard
                timeBeforeFirst = 10;
                anomalyCooldown = 15;
                SafeZoneTime = 76;
                makeTextRed = 74;
                anomalyPercentageIncrease = 3;
                finalAnomalyTime = 15;
                break;
            case 3: //Impossible
                timeBeforeFirst = 5;
                anomalyCooldown = 15;
                SafeZoneTime = 0;
                makeTextRed = 0;
                anomalyPercentageIncrease = 2;
                finalAnomalyTime = 10;
                break;
        }

    }
}
