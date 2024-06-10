using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text heroWinsCount;
    [SerializeField]
    private TMP_Text heroLoseCount;
    [SerializeField]
    private TMP_Text heroGamesCount;
    [SerializeField]
    private TMP_Text heroName;

    [SerializeField]
    private TMP_Text overallGamesCount;
    [SerializeField]
    private TMP_Text overallWinsCount;
    [SerializeField]
    private TMP_Text lvl;
    [SerializeField]
    private TMP_Text xp;
    void Start()
    {
        UpdateStatsPanel(0);
    }

    public void UpdateStatsPanel(int hero)
    {
        switch (hero)
        {
            case 0: 
                heroWinsCount.text = $"x {MainMenu.dts.foxWins}";
                heroLoseCount.text = $"x {MainMenu.dts.foxLoses}";
                heroGamesCount.text = $"x {MainMenu.dts.foxWins + MainMenu.dts.foxLoses}";
                heroName.text = $"Hero:Fox";
                break;
            case 1: 
                heroWinsCount.text = $"x {MainMenu.dts.pandaWins}";
                heroLoseCount.text = $"x {MainMenu.dts.pandaLoses}";
                heroGamesCount.text = $"x {MainMenu.dts.pandaWins + MainMenu.dts.pandaLoses}";
                heroName.text = $"Hero:Panda";
                break;
            case 2: 
                heroWinsCount.text = $"x {MainMenu.dts.spiderWins}";
                heroLoseCount.text = $"x {MainMenu.dts.spiderLoses}";
                heroGamesCount.text = $"x {MainMenu.dts.spiderWins + MainMenu.dts.spiderLoses}";
                heroName.text = $"Hero:Spider";
                break;
            default: 
                break;
        }

        overallGamesCount.text = $"{MainMenu.dts.foxWins + MainMenu.dts.foxLoses + MainMenu.dts.pandaWins + MainMenu.dts.pandaLoses +  MainMenu.dts.spiderWins + MainMenu.dts.spiderLoses} games";
        overallWinsCount.text = $"{MainMenu.dts.foxWins + MainMenu.dts.pandaWins + MainMenu.dts.spiderWins} wins";
        lvl.text = $"Lvl: {MainMenu.dts.lvl}";
        xp.text = $"XP:{MainMenu.dts.XP}/100";
    }
}
