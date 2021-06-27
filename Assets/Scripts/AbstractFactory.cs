using System;
using JetBrains.Annotations;
using UnityEngine;

public abstract class AbstractFactory<T> :MonoBehaviour , IFactory <T>  where T : MonoBehaviour
{
    [SerializeField][NotNull]
    protected T m_Prefab;

    public T CreateSingleton()
    {
        throw new NotImplementedException();
    }

    /// <summary>Creates an instance of the prefab with the type of factory attached to it </summary>
    public virtual T GetNewInstance()
    {
        return Instantiate(m_Prefab);
    }

    /// <summary>Creates an instance of the prefab with the type of factory attached to it </summary>
    /// <param name = "position"> position to spawn the prefab.</param>
    /// <param name = "rotation"> rotation of the spawned prefab </param>
    public virtual T GetNewInstance(Vector3 position, Quaternion rotation)
    {
        return Instantiate(m_Prefab, position, rotation);
    }
}
