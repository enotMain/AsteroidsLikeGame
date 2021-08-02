using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleAsteroid : AsteroidAbstract
{
    /// <summary>
    /// Middle asteroid's action after being destroyed
    /// </summary>
    public override void AfterDestroy()
    {
        AudioSource.PlayClipAtPoint(_explosion, Camera.main.transform.position, 0.05f);

        ObjectPooler.Instance.DestroyObject(gameObject);
        AsteroidLaunch.CurrAmountAsteroids--;

        ObjectPooler.ObjectInfo.ObjectType nextAsteroidType = ObjectPooler.ObjectInfo.ObjectType.SMA_ASTEROID;
        SpawnTwoSmallAsteroids(nextAsteroidType);
    }

    /// <summary>
    /// Spawn two small asteroids with 45 degrees angle
    /// </summary>
    /// <param name="nextAsteroidType">Type of spawning asteroids</param>
    private void SpawnTwoSmallAsteroids(ObjectPooler.ObjectInfo.ObjectType nextAsteroidType)
    {
        ObjectPooler.Instance.GetObject(nextAsteroidType)
            .GetComponent<AsteroidAbstract>().OnCreate(transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + 45));
        ObjectPooler.Instance.GetObject(nextAsteroidType)
            .GetComponent<AsteroidAbstract>().OnCreate(transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z - 45));
    }
}
