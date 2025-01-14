using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager INSTANCE;

    public string playerName;
    public int playerScore;
    public int playerWave;

    public string bestName;
    public int bestScore;
    public int bestWave;

    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    public class SaveBestData
    {
        public string bestName;
        public int bestScore;
        public int bestWave;
    }

    public void SaveBestScore()
    {
        SaveBestData data = new SaveBestData();
        data.bestName = bestName;
        data.bestScore = bestScore;
        data.bestWave = bestWave;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/bestScore.json", json);
    }

    public void LoadBestData()
    {
        string path = Application.dataPath + "/bestScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveBestData data = JsonUtility.FromJson<SaveBestData>(json);
            bestName = data.bestName;
            bestScore = data.bestScore;
            bestWave = data.bestWave;
        }
    }
}
