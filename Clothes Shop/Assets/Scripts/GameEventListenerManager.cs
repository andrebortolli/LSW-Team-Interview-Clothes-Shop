using ScriptableObjectExtensions.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListenerManager : MonoBehaviour
{
    //Singleton Struct
    private static GameEventListenerManager sInstance = null;

    public static GameEventListenerManager Instance
    {
        get { return sInstance; }
        private set { }
    }

    List<GameEventListener> gameEventListeners = new List<GameEventListener>();

    private void Awake()
    {
        if (sInstance == null)
        {
            sInstance = GetComponent<GameEventListenerManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
