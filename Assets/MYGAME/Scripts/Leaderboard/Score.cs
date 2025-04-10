using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Score : MonoBehaviour
{
    private List<PlayerInfo> players;
    public ScoreUI[] scoreUIlist;

    void Start()
    {
        players = ReadFromFile("playerData.json");
        if (players == null)
            players = new List<PlayerInfo>();

        DisplayScore();
    }

    private void WriteToFile(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        string json = JsonUtility.ToJson(new PlayerInfoList(players), true);
        File.WriteAllText(path, json);

        Debug.Log($"Data written to file at {path}");
    }

    private List<PlayerInfo> ReadFromFile(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerInfoList wrapper = JsonUtility.FromJson<PlayerInfoList>(json);

            Debug.Log("Data read from file:");
            foreach (var player in wrapper.players)
                Debug.Log($"{player.playerName}: {player.coins}");

            return wrapper.players;
        }
        else
        {
            Debug.LogWarning("File not found at path: " + path);
            return null;
        }
    }

    private void DisplayScore()
    {
        var topPlayers = players.OrderByDescending(p => p.coins).Take(10).ToList();

        string result = "";

        for (int i = 0; i < topPlayers.Count; i++)
        {
            var player = topPlayers[i];
            scoreUIlist[i].gameObject.SetActive(true);
            scoreUIlist[i].number.text = (i + 1).ToString();
            scoreUIlist[i].playerName.text = player.playerName;
            scoreUIlist[i].score.text = player.coins.ToString();
            
        }
    }

    // ���������� ������ ������
    public void AddPlayer(string name, int coins)
    {
        players.Add(new PlayerInfo(name, coins));
        WriteToFile("playerData.json");
    }
}
