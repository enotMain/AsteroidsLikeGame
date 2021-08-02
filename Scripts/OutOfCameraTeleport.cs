using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCameraTeleport : MonoBehaviour
{
    /// <summary>
    /// If object move out of the camera
    /// </summary>
    private void OnBecameInvisible()
    {
        try
        {
            Camera cam = Camera.main;
            Vector3 camPos = cam.WorldToViewportPoint(transform.position);
            if (!(camPos.x > 0 && camPos.x < 1 && camPos.y > 0 && camPos.y < 1))
            {
                SetNewObjectPosition();
            }
        }
        catch (NullReferenceException exception)
        {
            Debug.Log(exception.Message);
        }   
    }

    /// <summary>
    /// Set new object position when it is out of the camera
    /// </summary>
    private void SetNewObjectPosition()
    {
        transform.position = Camera.main.transform.position - (transform.position - Camera.main.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
