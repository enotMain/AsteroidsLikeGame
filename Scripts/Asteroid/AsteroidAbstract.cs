using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class AsteroidAbstract : OutOfCameraTeleport, IPooledObject
{
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType _type;  // Type of asteroid
    [SerializeField] private float _speed;                              // Speed of asteroid
    public AudioClip _explosion;                                        // Explosion sound
    private Rigidbody2D _rb;                                            // Rigidbody2D field

    public ObjectPooler.ObjectInfo.ObjectType Type { get => _type; set => _type = value; }  // Property of _type

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Set velocity of asteroid
    /// </summary>
    /// <param name="vector3">Direction of asteroid</param>
    /// <param name="speed">Speed of asteroid</param>
    public void SetVelocity(Vector3 vector3, float speed)
    {
        _rb.velocity = vector3 * speed;
    }

    /// <summary>
    /// Set transform and velocity on creation
    /// </summary>
    /// <param name="position">Position of asteroid</param>
    /// <param name="rotation">Rotation of asteroid</param>
    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        SetVelocity(transform.up, _speed);
        AsteroidLaunch.CurrAmountAsteroids++;
    }

    /// <summary>
    /// Action after asteroid's destroy
    /// </summary>
    public abstract void AfterDestroy();

    /// <summary>
    /// Trigger collision with object that destroy an asteroid
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UFO")
            || collision.CompareTag("Bullet")
            || collision.CompareTag("PlayerShip"))
        {
            AfterDestroy();
        }
    }
}
