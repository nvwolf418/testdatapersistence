using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HighScoreText;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private string m_CurrentName;
    private int m_CurrentPoints;
    private int m_HighScorePoints;
    private string m_HighScoreName = null;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
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
        NameText.text = MainMenuManager.nameText;
        m_CurrentName = MainMenuManager.nameText.Split(':')[1].Trim();
        GetHighScoreF();
        DisplayHighScore();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_CurrentPoints += point;
        ScoreText.text = $"Score : {m_CurrentPoints}";
        DisplayHighScore();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        CreateHighScoreF();
    }




    [System.Serializable]
    public class HighScore
    {
        public int score;
        public string name;
    }

    private void CreateHighScoreF()
    {
        Debug.Log($"file loc: {Application.persistentDataPath}savefilehighscore.json");
        if (m_CurrentPoints >= m_HighScorePoints)
        {
           
            HighScore highScore = new HighScore();
            highScore.score = m_CurrentPoints;
            highScore.name = m_CurrentName;

            string json = JsonUtility.ToJson(highScore);
            File.WriteAllText(Application.persistentDataPath + "savefilehighscore.json", json);
        }
    }

    private void GetHighScoreF()
    {

       
        string path = Application.persistentDataPath + "savefilehighscore.json";
        Debug.Log($"file path exists???: {File.Exists(path)} --- {Application.persistentDataPath}savefilehighscore.json");
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            HighScore highScore = JsonUtility.FromJson<HighScore>(json);

            m_HighScorePoints = highScore.score;
            m_HighScoreName = highScore.name;
        }
        else
        {
            m_HighScorePoints = 0;
        }
    }

    private void DisplayHighScore()
    {
        //sets high score display to current player if prev high score doesn't exist or it has been beaten
        if(m_HighScoreName == null || m_CurrentPoints > m_HighScorePoints)
        {
            HighScoreText.text = $"Name: {m_CurrentName} High Score: {m_CurrentPoints}";
        }
        else
        {
            HighScoreText.text = $"Name: {m_HighScoreName} High Score: {m_HighScorePoints}";
        }
    }
}
