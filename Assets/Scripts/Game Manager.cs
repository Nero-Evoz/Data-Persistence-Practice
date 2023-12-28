using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    public static GameManager Instance;

    //Player name
    public static string playerName;
    [SerializeField] TextMeshProUGUI playerNameInputBox;

    //Current high score (from load)
    public static int highScore;
    public static string highScoreName;

    private void Awake()
    {
        //LoadGame();
        //If there is already an instance, destroy new
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Things to be saved
    [Serializable]
    class SaveData
    {
        public string savedHighScoreName;
        public int savedHighScore;
    }

    //On end of game, if new score is higher than high score, save as new high score
    public void SaveGame(int score)
    {
        SaveData data = new SaveData();
        data.savedHighScoreName = playerName;
        data.savedHighScore = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //On scene load, get high score data
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.savedHighScore;
            highScoreName = data.savedHighScoreName;
        }
    }

    //Loads game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        playerName = playerNameInputBox.text;
        Debug.Log(playerName);
    }
}
