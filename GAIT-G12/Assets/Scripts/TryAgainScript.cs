﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainScript : MonoBehaviour
{
   public void playGame() {
        GameManagerScript.Instance.resetGame();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        print("LOADED");
    }
}
