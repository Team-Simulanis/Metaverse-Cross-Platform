
using System;
using MPUIKIT;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [ReadOnly]public AccessData accessData;

    public Image loginLogo;
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

    private void Start()
    {
        InitApplication();
    }

    private async void InitApplication()
    {
        var defaultData = await WebRequestManager.Instance.WebRequest("");
        UpdateBrandingDetails();
    }

    private async void UpdateBrandingDetails()
    {
        loginLogo.sprite = await WebRequestManager.Instance.ImageDownloadRequest("https://drive.google.com/uc?export=download&id=1irLG5jAF6w3qJG4Lota4cnWFiTJoDbQO");
    }
}

