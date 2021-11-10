using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public Text bestScoreText;
    public int bestScore;

    public string bestName;
    public string bestText;

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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

        LoadScore();

        bestText += "Best Score: " + bestName + ": " + bestScore;
        bestScoreText.text = bestText;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
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
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (m_Points > bestScore) { SaveScore(); }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    [System.Serializable]
    class SaveBest
    {
        public int savedScore;
        public string savedName;
    }

    public void SaveScore()
    {
        bestScore = m_Points;
        bestName = NameManager.Instance.pName;
        SaveBest data = new SaveBest();
        data.savedScore = bestScore;
        data.savedName = bestName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savebest.json", json);
        Debug.Log("High Score saved");
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savebest.json";
        if (File.Exists(path))
        {
            //Load Best 
            string json = File.ReadAllText(path);
            SaveBest data = JsonUtility.FromJson<SaveBest>(json);
            bestScore = data.savedScore;
            bestName = data.savedName;
            //sName.pName.text = bestScore;
            Debug.Log("High Score loaded");
        }
    }

}
