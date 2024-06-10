using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AnomalyManager am;
    public GameObject Tower;
    public GameObject Spawn;
    public GameObject Player;
    public Material MainMaterial;
    public Material TabMaterial;

    public float restartDelay = 1f;

    public bool ReachedTheTop = false;
    public bool GameOver = false;
    public static bool isWin = false;
    private bool slowmotion;
    // public bool AtTheStart = false; 

    static public int deathCounter;
    static public int COINScollected = 0;
    static public int collectedGold = 0;
    private int carryingGold = 0;
    public bool carryingChest = false;
    public string carryingChestType = "";
    static public int CHESTScarried = 0;
    static public string[] collectedChests;
    public TMP_Text deathsText;
    public TMP_Text COINStext;
    public TMP_Text CHESTStext;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    
    public float skyboxRotationSpeed = 0.2f;

    public static Character chosenCharacter = Character.spider;

    [SerializeField]
    private GameObject explode;
    [SerializeField]
    private FollowPlayer2 fp2;
    [SerializeField]
    private lookAtMe lam;

    //for Game Over Info
    public static bool showInfo = false;
    public enum Character
    {
        panda = 0,
        fox = 1,
        spider = 2
    }

    void Start()
    {


        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            deathCounter = 0;
            COINScollected = 0;
            CHESTScarried = 0;
            collectedGold = 0;
            collectedChests = new string[3];
            isWin = false;

        }
        Time.timeScale = 1; 
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", skyboxRotationSpeed * Time.time);

        if (Input.GetKeyDown("r") && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Respawn();
        }
        if (Input.GetKeyDown("1"))
        {
            Restart(); // потом убрать
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Pause();
        }

        if (CHESTScarried == 3 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Win();
        }

        if (GameOver)
        {
            showInfo = true;  //чтобы открывалось сначала меню результатов
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void Win()
    {
        WinPanel.SetActive(true);
        Player.GetComponent<newMovement>().forwardForce = 0;
        Player.GetComponent<newMovement>().sidewaysForce = 0;
        fp2.gameEnd = true;
        lam.gameEnd = true;
        isWin = true;
        Invoke("Over",  4f);
    }
    private void Over() { GameOver = true; }
    public void EndGame() //lose
    {
        GameOverPanel.SetActive(true);
        fp2.gameEnd = true;
        lam.gameEnd = true;
        Invoke("DestroyTower", 2.5f);
        Player.GetComponent<newMovement>().forwardForce = 0;
        Player.GetComponent<newMovement>().sidewaysForce = 0;
        Invoke("Over", 4f);
    }

    void DestroyTower()
    {
        Instantiate(explode);
        Destroy(Tower, 0.1f);
    }

    public void Reached(bool a)
    {
        ReachedTheTop = a;
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Respawn()
    {
        if (!GameOver) 
        {
            deathCounter += 1;
            deathsText.text = $": {deathCounter}";

            am.PlayerDied();
            Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Player.transform.eulerAngles = new Vector3(-90, 90, -90);
            Player.transform.position = Spawn.transform.position;
        }

    }

    public void Coin()
    {
        COINScollected++;
        COINStext.text = $": {COINScollected}";
    }
    public void ChestCollected(int goldEarned, string chestType)
    {
        carryingChest = true;
        carryingGold += goldEarned;
        carryingChestType = chestType;
    }
    public void ChestCarried()
    {
        carryingChest = false;
        CHESTScarried++;
        CHESTStext.text = $": {CHESTScarried}";
        collectedGold += carryingGold;
        carryingGold = 0;
        collectedChests[CHESTScarried - 1] = carryingChestType;
        ChestTrigger.chestTaken = false;
    }

    public void CharacterChoice(string charName)
    {
        switch (charName)
        {
            case "panda":
                chosenCharacter = Character.panda;
                break;
            case "fox":
                chosenCharacter = Character.fox;
                break;
            case "spider":
                chosenCharacter = Character.spider;
                break;
            default: break;
        }
    }


    public Character CharacterType()
    {
        return chosenCharacter;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        PausePanel.SetActive(!PausePanel.activeSelf);
        if (Time.timeScale == 0.5)
        {
            Time.timeScale = 0;
            slowmotion = true;
        }
        else if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else if (slowmotion == true) //?
        {
            Time.timeScale = 0.5f;
            slowmotion = false;
        }
        else
            Time.timeScale = 1;
    }

}

