using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


    public DefaultData defaultData = new();


    public UserData userData = new();

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