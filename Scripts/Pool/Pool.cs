using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Transform _container;           // Object pools' container's transform
    private Queue<GameObject> _objects;     // Queue of objects

    public Transform Container { get => _container; set => _container = value; }    // Property of _container
    public Queue<GameObject> Objects { get => _objects; set => _objects = value; }  // Property of _objects

    /// <summary>
    /// Contructor of the Pool class
    /// </summary>
    /// <param name="container">transform of a container of object pools</param>
    public Pool(Transform container)
    {
        Container = container;
        Objects = new Queue<GameObject>();
    }
}
