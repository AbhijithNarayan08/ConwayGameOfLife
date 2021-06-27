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

    public virtual T GetNewInstance()
    {
        return Instantiate(m_Prefab);
    }

    public virtual T GetNewInstance(Vector3 position, Quaternion rotation)
    {
        return Instantiate(m_Prefab, position, rotation);
    }
}
