using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Singleton { get; private set; }

    protected virtual void Awake()
    {
        if (Singleton != null)
        {
            Debug.LogWarning("Duplicate subclass of type " + typeof(T) + "! eliminating " + name + " while preserving " + Singleton.name);
            Destroy(gameObject);
        }
        else
        {
            Singleton = this as T;
        }
    }
}
