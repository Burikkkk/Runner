using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfoList
{
    public List<PlayerInfo> players;

    public PlayerInfoList(List<PlayerInfo> list)
    {
        players = list;
    }
}