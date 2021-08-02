using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLaunch : MonoBehaviour
{
    public static int CurrAmountAsteroids;                              // Current amount of asteroid in the scene

    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType _type;  // Type of spawning object
    [SerializeField] private GameObject[] _asteroidSpawners;            // List of asteroid spawners
    [SerializeField] private int _amountAsteroids;                      // Amount asteroids to create in the beginning

    /// <summary>
    /// Initializing fields
    /// </summary>
    private void Awake()
    {
        CurrAmountAsteroids = 0;
        _amountAsteroids = 2; 
    }

    /// <summary>
    /// Create new wave of asteroids
    /// </summary>
    private void Start()
    {
        InstantiateAsteroidWave();
    }

    /// <summary>
    /// Create new wave of steroids when current amount of them is zero
    /// </summary>
    private void Update()
    {
        if (CurrAmountAsteroids == 0)
        {
            _amountAsteroids++;
            CurrAmountAsteroids = 0;
            InstantiateAsteroidWave();
        }
    }

    /// <summary>
    /// Create new wave of asteroids
    /// </summary>
    private void InstantiateAsteroidWave()
    {
        int asteroidSpawnerNumber;
        float spawnAngle;
        GameObject asteroidGameObj;
        for(int i = 0; i < _amountAsteroids; i++)
        {
            asteroidGameObj = ObjectPooler.Instance.GetObject(_type);
            asteroidSpawnerNumber = Random.Range(0, _asteroidSpawners.Length);
            spawnAngle = Random.Range(-40, 40);
            switch (_asteroidSpawners[asteroidSpawnerNumber].tag)
            {
                case "AsteroidSpawnerTop":
                    asteroidGameObj.GetComponent<AsteroidAbstract>().OnCreate(_asteroidSpawners[asteroidSpawnerNumber]
                        .transform.position,
                        Quaternion.Euler(0, 0, 180 + spawnAngle));
                    break;
                case "AsteroidSpawnerBottom":
                    asteroidGameObj.GetComponent<AsteroidAbstract>().OnCreate(_asteroidSpawners[asteroidSpawnerNumber]
                        .transform.position,
                        Quaternion.Euler(0, 0, 0 + spawnAngle));
                    break;
                case "AsteroidSpawnerRight":
                    asteroidGameObj.GetComponent<AsteroidAbstract>().OnCreate(_asteroidSpawners[asteroidSpawnerNumber]
                        .transform.position,
                        Quaternion.Euler(0, 0, 90 + spawnAngle));
                    break;
                case "AsteroidSpawnerLeft":
                    asteroidGameObj.GetComponent<AsteroidAbstract>().OnCreate(_asteroidSpawners[asteroidSpawnerNumber]
                        .transform.position,
                        Quaternion.Euler(0, 0, -90 + spawnAngle));
                    break;
            }
        }
    }
}
