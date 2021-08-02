using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : AsteroidAbstract
{
    /// <summary>
    /// Small asteroid's action after being destroyed
    /// </summary>
    public override void AfterDestroy()
    {
        AudioSource.PlayClipAtPoint(_explosion, Camera.main.transform.position, 0.05f);
        ObjectPooler.Instance.DestroyObject(gameObject);
        AsteroidLaunch.CurrAmountAsteroids--;
    }
}
