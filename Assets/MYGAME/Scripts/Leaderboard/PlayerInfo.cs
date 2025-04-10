using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string playerName;
    public int coins;

    public PlayerInfo(string name, int coins)
    {
        this.playerName = name;
        this.coins = coins;
    }
}

