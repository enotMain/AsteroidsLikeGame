using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitUI : MonoBehaviour
{
    private Button _button; // Exit button field

    /// <summary>
    /// Initialize fields and add listener to the button
    /// </summary>
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TaskOnClick);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    private void TaskOnClick()
    {
        Application.Quit();
    }
}
