using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UFO : MonoBehaviour
{
    private float _speed;               // Speed of the UFO
    private Rigidbody2D _rb;            // Rigidbody2D component
    private bool _isToRightDirection;   // Should the UFO fly to the right direction

    /// <summary>
    /// Initialize the fields
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = Camera.main.orthographicSize * 2 
            / Screen.currentResolution.height * Screen.currentResolution.width / 10;
    }

    /// <summary>
    /// Set behavior of the UFO
    /// </summary>
    private void Start()
    {
        NewUFODirection();
        SetUFOPosition();
        SetVelocity(transform.right, _speed);
    }

    /// <summary>
    /// Check the UFO's position to destroy
    /// </summary>
    private void Update()
    {
        if (_isToRightDirection)
        {
            Vector3 camPos = Camera.main.WorldToViewportPoint(transform.position);
            if (camPos.x > 1) Destroy(gameObject);
        }
        else
        {
            Vector3 camPos = Camera.main.WorldToViewportPoint(new Vector3(transform.position.x
                + gameObject.transform.Find("Square").GetComponent<SpriteRenderer>().bounds.size.x, 0, 0));
            if (camPos.x < 0) Destroy(gameObject);
        }
    }

    /// <summary>
    /// Trigger to destroy the UFO
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("PlayerShip") || collision.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Set velocity of UFO
    /// </summary>
    /// <param name="vector3"> Direction </param>
    /// <param name="_speed"> Speed </param>
    private void SetVelocity(Vector3 vector3, float _speed)
    {
        if (_isToRightDirection) _rb.velocity = vector3 * _speed;
        else _rb.velocity = -vector3 * _speed;
    }

    /// <summary>
    /// Set position of UFO by task's condition
    /// </summary>
    private void SetUFOPosition() 
    {
        if (_isToRightDirection)
        {
            transform.position = new Vector3(Camera.main.transform.position.x -
                Camera.main.orthographicSize / Screen.currentResolution.height * Screen.currentResolution.width -
                gameObject.transform.Find("Square").GetComponent<SpriteRenderer>().bounds.size.x,
                Camera.main.transform.position.y - Camera.main.orthographicSize * 0.6f +
                Random.Range(0, Camera.main.orthographicSize * 1.2f -
                gameObject.transform.Find("Circle").GetComponent<SpriteRenderer>().bounds.size.y),
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Camera.main.transform.position.x +
                Camera.main.orthographicSize / Screen.currentResolution.height * Screen.currentResolution.width +
                gameObject.transform.Find("Square").GetComponent<SpriteRenderer>().bounds.size.x * 0.1f,
                Camera.main.transform.position.y - Camera.main.orthographicSize * 0.6f +
                Random.Range(0, Camera.main.orthographicSize * 1.2f -
                gameObject.transform.Find("Circle").GetComponent<SpriteRenderer>().bounds.size.y),
                transform.position.z);
        }
    }

    /// <summary>
    /// Randomize UFO's direction
    /// </summary>
    /// <returns> Right direction or not </returns>
    private bool NewUFODirection()
    {
        int random = Random.Range(0, 2);
        return _isToRightDirection = random == 0;
    }
}
