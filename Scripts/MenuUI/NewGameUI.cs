using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NewGameUI : MonoBehaviour
{
    private Button _button; // The new game button field

    /// <summary>
    /// Initialize fields and add listener to the button
    /// </summary>
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TaskOnClick);
    }

    /// <summary>
    /// Set new game options
    /// </summary>
    private void Start()
    {
        if (GameStatus.IsGameOn)
        {
            GameObject.Find("Canvas").GetComponent<PauseUI>().Resume();
            GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerShip>().IsBlinking = true;
            if (GameObject.Find("Control") != null) 
                GameObject.Find("Control").GetComponentInChildren<Text>().text = "”правление: клавиатура + мышь";
        }
        ScoreUI.Score = 0;
    }

    /// <summary>
    /// Start new game
    /// </summary>
    private void TaskOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameStatus.IsGameOn = true;
    }
}
