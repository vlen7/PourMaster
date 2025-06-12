using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindFirstObjectByType<T>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
    }
}