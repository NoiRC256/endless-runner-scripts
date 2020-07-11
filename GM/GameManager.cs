//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GM
public class GameManager : MonoBehaviour {

    ScoreManager scoreManager;
    ScrollManager scrollManager;
    //UIManager uiManager;
    PlayerHealth playerHealth;
    ObstacleSpawner spawner;
    //private bool gameState = true;

    //public Canvas playerCanvas;
    Canvas endCanvas;
    RectTransform scoreGroup;
    public Vector3 scoreGroupCenter = Vector3.zero;

    private void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
        scrollManager = GetComponent<ScrollManager>();
        //uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spawner = GetComponent<ObstacleSpawner>();

        //playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>();
        endCanvas = GameObject.FindGameObjectWithTag("EndCanvas").GetComponent<Canvas>();
        scoreGroup = GameObject.Find("ScoreGroup").GetComponent<RectTransform>();
        endCanvas.enabled = false;
    }

    private void FixedUpdate()
    {

        if (playerHealth.lose)
        {
            //gameState = false;
            scrollManager.StopScroll();
            spawner.StopAllCoroutines();

            Invoke("ShowEndCanvas", 1);
        }
        else
        {
            //gameState = true;
            scoreManager.AddDistance();
            spawner.minInterval = 0.5f / scrollManager.speedMultiplier;
        }
    }

    void ShowEndCanvas()
    {
        endCanvas.enabled = true;
        scoreGroup.localPosition = Vector3.Lerp(scoreGroup.localPosition, scoreGroupCenter, 3 * Time.deltaTime);
        scoreGroup.localScale = Vector3.Lerp(scoreGroup.localScale, new Vector3(1.2f,1.2f,1.2f), 3 * Time.deltaTime);
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
