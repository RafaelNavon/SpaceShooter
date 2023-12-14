using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private Image livesImg;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartGameText;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString();
        gameOverText.gameObject.SetActive(false);
        restartGameText.gameObject.SetActive(false);
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
