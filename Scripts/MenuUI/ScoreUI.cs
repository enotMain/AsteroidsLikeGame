using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour
{
    public static int Score;    // Score number
    private Text _text;         // Text that describes score number

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    /// <summary>
    /// Start score number
    /// </summary>
    private void Start()
    {
        _text.text = "Score: 0";
    }

    /// <summary>
    /// Listen to score number changing
    /// </summary>
    private void Update()
    {
        _text.text = "Score: " + Score;   
    }
}
