using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    [Serializable]
    public class LeaderBoard
    {
        public Player[] players;
    }

    [Serializable]
    public class Player
    {
        public int position = -1;
        public string name = "";
        public float time = 0.0f;
    }

    string path;
    LeaderBoard leaderBoard = null;

    private void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "LeaderBoard.json");
    }


    void Save()
    {
        OrderBoard();
        string data = JsonUtility.ToJson(leaderBoard);
        if (File.Exists(path))
        {
            File.WriteAllText(path, data);
        }
        else
        {
            File.Create(data);
            File.WriteAllText(path, data);
        }

        Debug.Log(data);
    }

    void Load ()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            leaderBoard = JsonUtility.FromJson<LeaderBoard>(data);
            OrderBoard();
            Debug.Log(data);

        }
        else
        {
            leaderBoard = new LeaderBoard();
            leaderBoard.players = new Player[8];
        }
    }

    void OrderBoard()
    {
        Array.Sort(leaderBoard.players, delegate (Player x, Player y) { return x.time.CompareTo(y.time); });
        
    }

    void Add(Player p)
    {
        
    }
}
