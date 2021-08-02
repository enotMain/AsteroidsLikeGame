using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueButton : MonoBehaviour
{
    private Button _button; // The button field

    /// <summary>
    /// Initializing the button and adding listener to it
    /// </summary>
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TaskOnClick);
    }

    /// <summary>
    /// Change the button posibility to be interectable depending of is there playing game
    /// </summary>
    private void Update()
    {
        if (GameStatus.IsGameOn) _button.interactable = true;
        else _button.interactable = false;
    }

    /// <summary>
    /// Resume the game when the button is pressed
    /// </summary>
    private void TaskOnClick()
    {
        GameObject.Find("Canvas").GetComponent<PauseUI>().Resume();
    }
}
