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

    private void Awake()
    {
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
        public string highScoreName;
        public int highScore;
    }

    public void SaveGame(int score)
    {
        SaveData data = new SaveData();
        data.highScoreName = playerName;
        data.highScore = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadGame()
    {

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
