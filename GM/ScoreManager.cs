//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//分数
public class ScoreManager : MonoBehaviour {

    UIManager uiManager;
    ScrollManager scrollManager;

    public int currentScore = 0;
    public int currentDistance = 0;
    float timer;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        scrollManager = GetComponent<ScrollManager>();
    }

    private void FixedUpdate()
    {
        uiManager.UpdateScore(currentScore);
        uiManager.UpdateDistance(currentDistance);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        if (currentScore > 0 && currentScore % 10 == 0 && scrollManager.speedMultiplier <= 0.95f)
        {
            scrollManager.IncreaseSpeed();
        }
    }

    public void AddDistance()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            currentDistance += Mathf.CeilToInt(2 * scrollManager.speedMultiplier);
            timer = 0;
        }
    }
}
