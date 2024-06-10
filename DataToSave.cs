using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataToSave
{
    public int coins;
    public int XP;
    public int lvl;
    public int foxWins;
    public int foxLoses;
    public int pandaWins;
    public int pandaLoses;
    public int spiderWins;
    public int spiderLoses;

    public DataToSave()
    {
        coins = 0;
        XP = 0;
        lvl = 0;
        foxWins = 0;
        foxLoses = 0;
        pandaWins = 0;
        pandaLoses = 0;
        spiderWins = 0;
        spiderLoses = 0;
    }

    public DataToSave(int _coins, int _XP, int _lvl, int _foxWins, int _foxLoses, int _pandaWins, int _pandaLoses, int _spiderWins, int _spiderLoses)
    {
        coins = _coins;
        XP = _XP;
        lvl = _lvl;
        foxWins = _foxWins;
        foxLoses = _foxLoses;
        pandaWins = _pandaWins;
        pandaLoses = _pandaLoses;
        spiderWins = _spiderWins;
        spiderLoses = _spiderLoses;
    }
}
