
using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [EnumToggleButtons][HideLabel][BoxGroup("Application Mode",CenterLabel = true)]
    public GameMode gameMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum GameMode
{
    Development,
    Production,
    AuthFree
}
