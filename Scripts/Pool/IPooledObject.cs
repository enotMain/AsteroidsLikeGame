using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public ObjectPooler.ObjectInfo.ObjectType Type { get; } // Type of gameobject in object pooler
}
