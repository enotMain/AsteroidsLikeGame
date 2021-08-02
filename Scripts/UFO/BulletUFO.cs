using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUFO : BulletPol
{
    /// <summary>
    /// Override OnCreate method (action on creation)
    /// </summary>
    /// <param name="position"> New position of the object </param>
    /// <param name="rotation"> New rotation of the object </param>
    public override void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        SetVelocity((GameObject.FindGameObjectWithTag("PlayerShip")
            .transform.position - transform.position).normalized, Speed);
    }

    /// <summary>
    /// Destroy condition
    /// </summary>
    /// <param name="collision"> An object that the bullet triggers with </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("PlayerShip"))
        {
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }
}
