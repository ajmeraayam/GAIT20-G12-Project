using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
    private static GameManagerScript instance = null;
    private GameObject gameOverUI;
    public int score = 0;
    public int playerHealth = 100;
    public bool isMenu = true, gameOver = false;

    public static GameManagerScript Instance {
        get {
            if (instance == null) {
                GameObject GameManagerObject = new GameObject();
                GameManagerObject.name = "GameManager";

                instance = GameManagerObject.AddComponent<GameManagerScript>();
            }
            return instance;
        }

    }


    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (playerHealth <= 0) {
            GameOver();
        }
    }
    public void GameOver() {

        //Load the game over screen
        gameOverUI = (GameObject)Resources.Load("Prefabs/GameOverCanvas");
        Instantiate(gameOverUI);
        score = 0;
        playerHealth = 100;
    }
}
