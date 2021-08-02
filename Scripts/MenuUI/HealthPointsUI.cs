using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthPointsUI : MonoBehaviour
{
    public static int HealthPoints; // Current amount of health points

    private Image[] _images;        // UI that shows current amount of health points

    /// <summary>
    /// Initialize fields and set start amount of health points
    /// </summary>
    private void Awake()
    {
        HealthPoints = 4;
        _images = GetComponentsInChildren<Image>();
    }

    /// <summary>
    /// Set changing amount of health points and finish a game when a player loses
    /// </summary>
    private void Update()
    {
        if (HealthPoints <= 3 && HealthPoints >= 0) _images[HealthPoints].gameObject.SetActive(false);
        if (HealthPoints <= 0 && GameObject.FindGameObjectWithTag("PlayerShip") != null)
        {
            FinishGame();
        }
    }

    /// <summary>
    /// Set some parameters to a game off status
    /// </summary>
    private void FinishGame()
    {
        GameObject.Find("Canvas").GetComponent<PauseUI>().Pause();
        GameObject.Find("Continue").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("PlayerShip").SetActive(false);
        PauseUI.GameIsPaused = true;
        GameStatus.IsGameOn = false;
    }
}
