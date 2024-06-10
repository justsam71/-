using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class time : MonoBehaviour
{
    static public float timer;
    private float flashTimer;
    private float flashDuration = 1f;
    bool GameOver = false;

    [SerializeField]
    public TextMeshProUGUI firstMinute;
    [SerializeField]
    public TextMeshProUGUI secondMinute;
    [SerializeField]
    public TextMeshProUGUI separator;
    [SerializeField]
    public TextMeshProUGUI firstSecond;
    [SerializeField]
    public TextMeshProUGUI secondSecond;


    void Start()
    {
        ResetTimer();
    }

    void Update()
    { // if player reached hasn't reached the top or fell down from the top
        GameOver = FindObjectOfType<GameManager>().GameOver;

        if (GameOver == false) // 
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        } else {
            Flash();
        }
    }

    private void ResetTimer()
    {
        timer = 0;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        } else if (flashTimer >=flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        } else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
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

    //public void PlayerReachedTheTop()
    //{
    //    TopTriggered = true;
    //}
    //public void PlayerFell()
    //{
    //    TopTriggered = false;
    //    SetTextDisplay(true);
    //}
}
