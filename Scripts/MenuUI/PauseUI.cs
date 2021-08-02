using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static bool GameIsPaused = true; // The game is paused status
    private GameObject _menuUI;             // Full menu UI

    /// <summary>
    /// Initialize the fields
    /// </summary>
    private void Awake()
    {
        Time.timeScale = 0f;
        _menuUI = gameObject.transform.Find("MenuUI").gameObject;
    }

    /// <summary>
    /// Listen to escape button is pressed
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    /// <summary>
    /// Resume the game
    /// </summary>
    public void Resume()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
