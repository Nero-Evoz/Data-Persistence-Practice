using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text highScore;
    public GameObject congratsText;
    
    private bool m_Started = false;
    public int m_Points = 0;
    
    private bool m_GameOver = false;

    string playerName;


    private void Awake()
    {
        playerName = GameManager.playerName;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set current name and score in text
        ScoreText.text = "Name: " + playerName + " --- Score: " + m_Points;

        
        //Set high score and name in high score text
        highScore.text = "High Score: " + GameManager.highScore + " Name: " + GameManager.highScoreName;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            //If beaten old score
            if (m_Points > GameManager.highScore)
            {
                congratsText.SetActive(true);
                GameManager.Instance.SaveGame(m_Points);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                #if UNITY_EDITOR
                    EditorApplication.ExitPlaymode();
                #else
                Application.Quit();
                #endif
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = "Name: " + playerName + " --- Score: " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }




}
