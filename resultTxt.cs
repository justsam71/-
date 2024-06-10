using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resultTxt : MonoBehaviour
{
    [SerializeField]
    private TMP_Text mainText;
    private string text;
    private int i;

    [SerializeField]
    private GameObject panda;
    [SerializeField]
    private GameObject fox;
    [SerializeField]
    private GameObject spider;

    [SerializeField]
    private GameObject[] chests;
    [SerializeField]
    private Transform[] SpawnPoint;
    private int chestNum = 0; //specific number
    private int chestCount;
    [SerializeField]
    private StatsPanel statsPanel;

    void Start()
    {
        SaveResult();

        chestCount = GameManager.CHESTScarried;
        text = $"Level: {MainMenu.chosenLevel} - {MainMenu.chosenDifficulty} \n Chests: {GameManager.CHESTScarried}\n Coins: {GameManager.COINScollected}" +
            $"\n Deaths: {GameManager.deathCounter} \n Time: {time.timer} s \n Total Gold: {GameManager.collectedGold}";

        i = 0;
        Invoke("WriteTextSlowly", 0.5f);
        if (chestCount > 0)
        {
            Invoke("ThrowChests", 2f);
        }
        switch ((int)GameManager.chosenCharacter)
        {
            case (0):
                panda.SetActive(true);
                break;
            case (1):
                fox.SetActive(true);
                break;
            case (2):
                spider.SetActive(true);
                break;
        }
    }

    void WriteTextSlowly()
    {
        if (i < text.Length)
        {
            mainText.text = mainText.text + text[i];
            i += 1;
            Invoke("WriteTextSlowly", 0.05f);
        }

    }



    void ThrowChests()
    {
        if (GameManager.collectedChests[chestNum] == "gradeOneChest")
        {
            Instantiate(chests[0], SpawnPoint[chestNum]);
        }
        else if (GameManager.collectedChests[chestNum] == "gradeTwoChest")
        {
            Instantiate(chests[1], SpawnPoint[chestNum]);
        }
        else if (GameManager.collectedChests[chestNum] == "gradeThreeChest")
        {
            Instantiate(chests[2], SpawnPoint[chestNum]);
        }
        chestNum++;
        if (chestCount > chestNum)
            Invoke("ThrowChests", 0.5f);

    }

    void SaveResult()
    {
        MainMenu.dts.coins += GameManager.collectedGold;

        MainMenu.dts.XP += GameManager.collectedGold * 2;

        if (MainMenu.dts.XP >= 100)
        {
            MainMenu.dts.lvl += 1;
            MainMenu.dts.XP -= 100;
        }

        if (GameManager.isWin) 
        {
            switch ((int)GameManager.chosenCharacter)
            {
                case 0:
                    MainMenu.dts.pandaWins += 1;
                    break;
                case 1:
                    MainMenu.dts.foxWins += 1;
                    break;
                case 2:
                    MainMenu.dts.spiderWins += 1;
                    break;
            }
        }
        else
        {
            switch ((int)GameManager.chosenCharacter)
            {
                case 0:
                    MainMenu.dts.pandaLoses += 1;
                    break;
                case 1:
                    MainMenu.dts.foxLoses += 1;
                    break;
                case 2:
                    MainMenu.dts.spiderLoses += 1;
                    break;
            }
        }
        MainMenu.dataManager.SaveData<DataToSave>("/player-stats.json", MainMenu.dts, true);
        statsPanel.UpdateStatsPanel((int)GameManager.chosenCharacter);
    }
}
