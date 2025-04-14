using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;
    private string _currentPlayerLevel;
    public int _currentPlayerIndex;

    void Awake()
    {
        _currentPlayerIndex = PlayerPrefs.GetInt(_currentPlayerLevel, 0);
    }

    void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }
    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
            GoToNextLevel();
    }

    void GoToNextLevel()
    {
        PlayerPrefs.SetInt(_currentPlayerLevel, SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return false;

        return true;
    }
}
