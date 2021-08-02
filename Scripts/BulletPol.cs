using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletPol : OutOfCameraTeleport, IPooledObject
{
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType _type;  // Type of object
    [SerializeField] private float _speed;                              // Speed of bullet
    private Rigidbody2D _rb;                                            // Rigidbody2D component
    private float _maxDistance;                                         // Maximum distance move

    public ObjectPooler.ObjectInfo.ObjectType Type { get => _type; set => _type = value; }  // Property of _type
    public float Speed { get => _speed; }                                                   // Property of _speed

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _maxDistance = Camera.main.orthographicSize * 2 
            / Screen.currentResolution.height * Screen.currentResolution.width;
    }

    /// <summary>
    /// Count distance the bullet reaches
    /// </summary>
    private void Update()
    {
        _maxDistance -= _rb.velocity.magnitude * Time.deltaTime;
        if (!(_maxDistance > 0))
        {
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }

    /// <summary>
    /// Set velocity of bullet
    /// </summary>
    /// <param name="vector3"> Direction </param>
    /// <param name="speed"> Speed of bullet </param>
    public void SetVelocity(Vector3 vector3, float speed)
    {
        _rb.velocity = vector3 * speed;
    }

    /// <summary>
    /// Action on creation
    /// </summary>
    /// <param name="position"> New position of the object </param>
    /// <param name="rotation"> New rotation of the object </param>
    public virtual void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        SetVelocity(transform.up, 0);
    }

    /// <summary>
    /// Reset maximum distance of the bullet on being set active
    /// </summary>
    private void OnEnable()
    {
        _maxDistance = Camera.main.orthographicSize * 2 / Screen.currentResolution.height * Screen.currentResolution.width;
    }
}
