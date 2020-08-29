using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour 
{
    //Singleton pattern - Only one instance of this class for the game
    private static GameManagerScript instance;
    // Game over and Game won screens stored in these gameobjects
    public GameObject gameOverUI, gameWonUI;
    public int score = 0;
    public int playerHealth = 100;
    private bool gameOver = false;
    // List to store gameobjects of humans that are saved
    private List<GameObject> humansSaved;

    // Returning the only instance for this class
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
        humansSaved = new List<GameObject>();
    }

    /*
     * When the Player dies, the game over screen appears over the game scene with the text saying
     * Game Over and a button to restart the level
     */
    public void GameOver() {
        Time.timeScale = 0;

        calcFinalScore(false);
        Instantiate(gameOverUI);
    }

    /*
     * When the Player reaches the safehouse, the game won screen appears over the game scene 
     * with the text saying Game Won, Final score and a button to restart the level
     */
    public void GameWon() {
        Time.timeScale = 0;

        calcFinalScore(true);
        Instantiate(gameWonUI);
    }

    // Increase current score
    public void increaseScore(int points) 
    {
        score += points;
    }

    // Restart the level
    public void resetGame() {
        gameOver = false;
        Time.timeScale = 1;
        score = 0;
        playerHealth = 100;
    }

    // Reduce the health of the player and check if the health is 0 or not
    public void reducePlayerHealth(int lostHealth) {
        playerHealth -= lostHealth;
        checkGameOver();
    }

    // Checks if the health points are 0 or lower than 0. If so, then game will end 
    private void checkGameOver() {
        if (playerHealth <= 0) {
            GameOver();
        }
    }

    /* 
     * Calculate the final score.
     * If Player won the game, then add bonus points to the score. Each human saved is 5 bonus points
     * If Player lost the game, then no bonus points added to the score 
     */ 
    private void calcFinalScore(bool won) 
    {
        if(won)
        {
            int bonusScore = humansSaved.Count * 5;
            score += bonusScore;
        }
    }

    // Add human gameobject to the list
    public void AddHumanToList(GameObject human)
    {
        humansSaved.Add(human);
    }

    // Remove human gameobject if it exists in the list
    public void RemoveHumanFromList(GameObject human)
    {
        for(int i = 0; i < humansSaved.Count; i++)
        {
            if(humansSaved[i].GetInstanceID() == human.GetInstanceID())
            {
                humansSaved.RemoveAt(i);
            }
        }
    }
}
