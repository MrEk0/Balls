using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float gameTime = 30f;
    [SerializeField] float speedMultiplier = 2f;
    [SerializeField] float speedUpdateFrequency = 5f;
    [SerializeField] Text scoreText=null;
    [SerializeField] Text timeText=null;
    [SerializeField] Canvas canvas=null;
    [SerializeField] GameObject gameOverPanel=null;

    public static GameManager instance = null;

    // variables
    private float score = 0;
    private float timeSinceLastUpdate = 0f;
    private bool isPanelSpawned = false;

    //obj variables
    private GameObject panel;
    private List<GameObject> balls;

    //event
    public event Action<float> OnTimeDown;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        balls = new List<GameObject>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
        timeText.text = gameTime.ToString();
    }

    private void Update()
    {
        TimerUpdate();
        SpeedUpdate();
    }

    private void TimerUpdate()
    {
        gameTime -= Time.deltaTime;

        timeText.text = string.Format("Time:  {0}s", Mathf.Round(gameTime));

        if (gameTime <= 0)
        {
            GameOver();
        }
    } 

    private void SpeedUpdate()
    {
        if (timeSinceLastUpdate > speedUpdateFrequency)
        {
            OnTimeDown(speedMultiplier);
            timeSinceLastUpdate = 0f;
        }
        timeSinceLastUpdate += Time.deltaTime;
    }

    public void AddBall(GameObject ball)
    {
        balls.Add(ball);
        ball.GetComponent<BallItem>().OnTouch += ChangeScore;
    }

    public void RemoveBall(GameObject ball)
    {
        balls.Remove(ball);
        ball.GetComponent<BallItem>().OnTouch -= ChangeScore;
    }

    private void ChangeScore(float score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    } 

    private void GameOver()
    {
        gameTime = 0f;
        if (!isPanelSpawned)
        {
            ShowGameOverPanel();
            isPanelSpawned = true;
        }
    }
    private void ShowGameOverPanel()
    {
        panel = Instantiate(gameOverPanel, canvas.transform);
        panel.transform.Find("FinalScore").GetComponent<Text>().text = string.Format("Your final score is:  {0}", score);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(panel);
        isPanelSpawned = false;
    } 
}
