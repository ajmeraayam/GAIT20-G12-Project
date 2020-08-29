using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
    private static GameManagerScript instance;
    public GameObject gameOverUI, gameWonUI;
    public int score = 0;
    public int playerHealth = 100;
    private bool gameOver = false;

    public static GameManagerScript Instance {
        get {
            return instance;
        }

    }


    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start() {
        instance = GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update() {
    }


    public void GameOver() {
        Time.timeScale = 0;

        calcFinalScore();
        Instantiate(gameOverUI);
    }

    public void GameWon() {
        Time.timeScale = 0;

        calcFinalScore();
        Instantiate(gameWonUI);
        

    }
    public void increaseScore(int points) {
        score += points;
    }

    public void resetGame() {
        gameOver = false;
        Time.timeScale = 1;
        score = 0;
        playerHealth = 100;
    }
    public void reducePlayerHealth(int lostHealth) {
        playerHealth -= lostHealth;
        checkGameOver();
    }
    private void checkGameOver() {
        if (playerHealth <= 0) {
            GameOver();
        }
    }

    private void calcFinalScore() {

    }
}
