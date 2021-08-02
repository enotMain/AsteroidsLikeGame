using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUFOLaunch : MonoBehaviour
{
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType _type;  // Type of object
    private float _timeLaunchBulletUFO;                                 // Time to launch new UFO bullet
    private float _launchTimeCounter;                                   // Counter of time
    private GameObject _bulletUFOObj;                                   // UFO bullet prefab
    private bool _createNewBulletUFO;                                   // Is time to create new bullet

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Awake()
    {
        _launchTimeCounter = 0f;
        _createNewBulletUFO = true;
        _bulletUFOObj = null;
        _timeLaunchBulletUFO = Random.Range(2f, 5f);
    }

    /// <summary>
    /// Launching UFO bullets logic
    /// </summary>
    private void Update()
    {
        if (_createNewBulletUFO)
        {
            _bulletUFOObj = ObjectPooler.Instance.GetObject(_type);
            _createNewBulletUFO = false;
        }
        if (_bulletUFOObj != null && _launchTimeCounter >= _timeLaunchBulletUFO)
        {
            _bulletUFOObj.GetComponent<BulletUFO>().OnCreate(transform.position, transform.rotation);
            _launchTimeCounter = 0f;
            _bulletUFOObj = null;
            _createNewBulletUFO = true;
        }
        _launchTimeCounter += Time.deltaTime;
    }
}
