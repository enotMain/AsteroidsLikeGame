using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _UFO;   // UFO prefab
    private float _spawnTime;                   // Spawning time
    private float _timer;                       // Time counter

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Start()
    {
        _spawnTime = Random.Range(20f, 40f);
        _timer = 0;
    }

    /// <summary>
    /// UFO spawning logic
    /// </summary>
    private void Update()
    {
        if (_timer >= _spawnTime)
        {
            Instantiate(_UFO);
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}
