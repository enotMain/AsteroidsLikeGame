using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerShip : OutOfCameraTeleport
{
    [SerializeField] private float _maxSpeed;                   // Maximum of player ship speed
    [SerializeField] private AudioSource _shipSpeedBoostSound;  // Sound of speed boost
    private Rigidbody2D _rb;                                    // Rigidbody2D component
    private SpriteRenderer _sr;                                 // SpriteRenderer component
    private float _speed;                                       // Speed of the player ship
    private bool _isBlinking;                                   // Is the ship blinking
    private bool _isImmortal;                                   // Is the ship immortal

    public bool IsBlinking { set => _isBlinking = value; }      // Property of _isBlinking

    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        SetShipToStartCondition();
    }

    /// <summary>
    /// Respawning blinking coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator RespawnBlinking()
    {
        yield return new WaitForSeconds(0.3f);
        _sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _sr.enabled = true;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);
            _sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            _sr.enabled = true;
        }

        _isImmortal = false;
    }

    /// <summary>
    /// Control the ship
    /// </summary>
    private void Update()
    {
        if (_isBlinking)
        {
            StartCoroutine(RespawnBlinking());
            _isBlinking = false;
        }

        if (ControlType.IsOnlyKeyBoard)
        {
            KeyboardControl();
        }
        else
        {
            KeyboardMouseControl();
        }

        _rb.AddForce(transform.up * _speed * Time.deltaTime);
    }

    /// <summary>
    /// Control the ship within only keyboard
    /// </summary>
    private void KeyboardControl()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && _speed < _maxSpeed)
        {
            if (!_shipSpeedBoostSound.isPlaying) _shipSpeedBoostSound.Play();
            _speed += 20000f * Time.deltaTime;
            if (_speed > _maxSpeed) _speed = 5000f;
        }
        else if (_speed > 0)
        {
            _speed -= 20000f * Time.deltaTime;
            if (_speed < 0) _speed = 0;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, 300f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -300f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Control the ship within keyboard and mouse
    /// </summary>
    private void KeyboardMouseControl()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Mouse1))
                && _speed < _maxSpeed)
        {
            if (!_shipSpeedBoostSound.isPlaying) _shipSpeedBoostSound.Play();
            _speed += 20000f * Time.deltaTime;
            if (_speed > _maxSpeed) _speed = 5000f;
        }
        else if (_speed > 0)
        {
            _speed -= 20000f * Time.deltaTime;
            if (_speed < 0) _speed = 0;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - transform.position;
        transform.up = Vector2.MoveTowards(transform.up, aimDirection, Time.deltaTime * 7f);
    }

    /// <summary>
    /// Trigger hitting the ship by enemies
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Asteroid")
            || collision.CompareTag("UFO")
            || collision.CompareTag("BulletUFO")) && _isImmortal == false)
        {
            HealthPointsUI.HealthPoints--;
            SetShipToStartCondition();
        }
    }
    
    /// <summary>
    /// Nullify the ship
    /// </summary>
    private void SetShipToStartCondition()
    {
        transform.position = new Vector3(Camera.main.transform.position.x,
                Camera.main.transform.position.y, 0);
        _isBlinking = true;
        _speed = 0;
        _rb.velocity = Vector3.zero;
        _isImmortal = true;
    }
}