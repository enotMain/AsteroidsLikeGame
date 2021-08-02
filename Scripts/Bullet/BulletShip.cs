using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShip : BulletPol
{
    [SerializeField] private int _bigAsteroidScore;
    [SerializeField] private int _midAsteroidScore;
    [SerializeField] private int _smaAsteroidScore;
    [SerializeField] private int _UFOScore;
 
    /// <summary>
    /// Override OnCreate method (action on creation)
    /// </summary>
    /// <param name="position"> New position of the object </param>
    /// <param name="rotation"> New rotation of the object </param>
    public override void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        SetVelocity(transform.up, Speed);
    }

    /// <summary>
    /// Destroy condition
    /// </summary>
    /// <param name="collision"> An object that the bullet triggers with </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") && collision.gameObject.GetComponent<AsteroidAbstract>().Type ==
            ObjectPooler.ObjectInfo.ObjectType.BIG_ASTEROID)
        {
            ScoreUI.Score += _bigAsteroidScore;
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
        else if (collision.CompareTag("Asteroid") && collision.gameObject.GetComponent<AsteroidAbstract>().Type ==
            ObjectPooler.ObjectInfo.ObjectType.MID_ASTEROID)
        {
            ScoreUI.Score += _midAsteroidScore;
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
        else if (collision.CompareTag("Asteroid") && collision.gameObject.GetComponent<AsteroidAbstract>().Type ==
            ObjectPooler.ObjectInfo.ObjectType.SMA_ASTEROID)
        {
            ScoreUI.Score += _smaAsteroidScore;
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
        else if (collision.CompareTag("UFO")) 
        {
            ScoreUI.Score += _UFOScore;
            ObjectPooler.Instance.DestroyObject(gameObject);
        } 
    }
}