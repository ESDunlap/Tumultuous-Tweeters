using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    LevelController levelController;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void continueGame()
    {
        levelController= FindObjectOfType<LevelController>();
        SceneManager.LoadScene(levelController._currentPlayerIndex);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
}
