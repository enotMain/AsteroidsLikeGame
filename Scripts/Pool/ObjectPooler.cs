using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public struct ObjectInfo    // Describes base information of objects
    {
        public enum ObjectType
        {
            BIG_ASTEROID,
            MID_ASTEROID,
            SMA_ASTEROID,
            BULLET,
            BULLET_UFO
        }

        [SerializeField] private ObjectType _type;
        [SerializeField] GameObject _prefab;
        [SerializeField] private int _startCount;

        public ObjectType Type { get => _type; set => _type = value; }
        public GameObject Prefab { get => _prefab; set => _prefab = value; }
        public int StartCount { get => _startCount; set => _startCount = value; }
    }

    [SerializeField] private List<ObjectInfo> _objectsInfo; // List of different object types
    private Dictionary<ObjectInfo.ObjectType, Pool> _pools; // Pools of objects
    private static ObjectPooler _instance;                  // Singleton

    public static ObjectPooler Instance { get => _instance;
        set => _instance = value; }                         // Property of singleton

    /// <summary>
    /// Initialize sibgleton and pool
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InitPool();
    }

    /// <summary>
    /// Create and fill pools
    /// </summary>
    private void InitPool()
    {
        _pools = new Dictionary<ObjectInfo.ObjectType, Pool>();
        var emptyGameObj = new GameObject();

        foreach (var obj in _objectsInfo)
        {
            var container = Instantiate(emptyGameObj, transform, false);
            container.name = obj.Type.ToString();

            _pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type, container.transform);
                _pools[obj.Type].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGameObj);
    }

    /// <summary>
    /// Create new object for pool
    /// </summary>
    /// <param name="type"> Type of the object </param>
    /// <param name="parent"> Parent for the object (container) </param>
    /// <returns></returns>
    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var gameObj = Instantiate(_objectsInfo.Find(x => x.Type == type).Prefab, parent);
        gameObj.SetActive(false);
        return gameObj;
    }

    /// <summary>
    /// Get object from the pool
    /// </summary>
    /// <param name="type"> Type of the object </param>
    /// <returns></returns>
    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        var obj = _pools[type].Objects.Count > 0 ?
            _pools[type].Objects.Dequeue() : InstantiateObject(type, _pools[type].Container);
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// Return the object to the pool
    /// </summary>
    /// <param name="obj"> The object </param>
    public void DestroyObject(GameObject obj)
    {
        _pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }
}
