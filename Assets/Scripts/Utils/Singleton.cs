using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this as T;
        else DestroyImmediate(gameObject);
    }
    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
