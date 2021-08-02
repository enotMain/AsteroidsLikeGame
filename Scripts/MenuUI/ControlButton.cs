using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ControlButton : MonoBehaviour
{
    private Button _button; // The button field

    /// <summary>
    /// Initializing fields, setting text of the button and adding listener to the button
    /// </summary>
    private void Awake()
    {
        _button = GetComponent<Button>();
        if (ControlType.IsOnlyKeyBoard) _button.GetComponentInChildren<Text>().text = "”правление: клавиатура";
        else _button.GetComponentInChildren<Text>().text = "”правление: клавиатура + мышь";
        _button.onClick.AddListener(TaskOnClick);
    }

    /// <summary>
    /// Change control type and text of the button
    /// </summary>
    private void TaskOnClick()
    {
        if (ControlType.IsOnlyKeyBoard)
        {
            ControlType.IsOnlyKeyBoard = false;
            _button.GetComponentInChildren<Text>().text = "”правление: клавиатура + мышь";
        }
        else 
        {
            ControlType.IsOnlyKeyBoard = true;
            _button.GetComponentInChildren<Text>().text = "”правление: клавиатура";
        }
    }
}
