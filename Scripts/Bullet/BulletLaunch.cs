using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletLaunch : MonoBehaviour
{
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType _type;  // Type of spawning object
    [SerializeField] private float _timeLaunchBullet;                   // Type to launch the bullet
    [SerializeField] private AudioSource _audioSource;                  // Sound of shot

    private float _launchTimeCounter;                                   // Counter of launching
    private bool _createNewBullet;                                      // Is time to create new bullet object
    private GameObject _bulletObj;                                      // Next bullet object to launch

    /// <summary>
    /// Initializing some fields
    /// </summary>
    private void Awake()
    {
        _launchTimeCounter = 0f;
        _createNewBullet = true;
    }

    /// <summary>
    /// Bullets' launching logic
    /// </summary>
    private void Update()
    {
        if (_createNewBullet)
        {
            _createNewBullet = false;
        }
        if (ControlType.IsOnlyKeyBoard)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _launchTimeCounter >= _timeLaunchBullet)
            {
                _bulletObj = ObjectPooler.Instance.GetObject(_type);
                _bulletObj.GetComponent<BulletShip>().OnCreate(transform.position, transform.rotation);
                _audioSource.Play();
                _launchTimeCounter = 0f;
                _createNewBullet = true;
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && _launchTimeCounter
                >= _timeLaunchBullet)
            {
                _bulletObj = ObjectPooler.Instance.GetObject(_type);
                _bulletObj.GetComponent<BulletShip>().OnCreate(transform.position, transform.rotation);
                _audioSource.Play();
                _launchTimeCounter = 0f;
                _createNewBullet = true;
            }
        }

        _launchTimeCounter += Time.deltaTime;
    }
}
