using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPannel;
    public Text scoreText;
    public Text bestScoreText;

    
    public Text messageText;

    public bool isRoundActive = false;
    private int score = 0;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    private void Start()
    {
        StartCoroutine("RoundRoutine");
    }

    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if(GetBestScore() <  score)
        {
            PlayerPrefs.SetInt("BestScore", score); //key, value½Ö
        }
       
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }
    // Update is called once per frame
    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "Best Score : " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
        onReset.Invoke();
        //Ready
        readyPannel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;
        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

        //PLAY
        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while(isRoundActive == true)
        {
            yield return null;
        }

        readyPannel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait for next round...";

        //END
        yield return new WaitForSeconds(3f);
        Reset();
    }
}
