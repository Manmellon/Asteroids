using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance => _instance;

    void Awake()
    {
        _instance = this as T;
    }
}
