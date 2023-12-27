using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
