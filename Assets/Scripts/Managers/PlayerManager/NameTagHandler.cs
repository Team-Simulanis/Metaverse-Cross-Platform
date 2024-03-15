using FF;
using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NameTagHandler : NetworkBehaviour
{
    public TextMeshProUGUI inGameUsername;
    
    private void Start()
    {
        GetComponent<RPMPlayerManager>().onAvatarLoaded.AddListener(SetUserName);
    }

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    private void SetUsernameOnHost(string username)
    {
        inGameUsername.text = username;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetUsernameOnServer(string playerName) //will call the observer RPC to sync the username to other player
    {
        SetUsernameOnHost(playerName);
    }

    private void SetUserName() //will be called on start to set the username of the player
    {
        if (!IsOwner) return;
        Debug.Log("Am Owner");
        if(DataManager.Instance)
        {
            SetUsernameOnServer(DataManager.Instance.userData.name);
        }
        else
        {
            SetUsernameOnServer($"Player+{RandomIDGenerator.GenerateRandomID()}");
        }
        inGameUsername.transform.parent.gameObject.SetActive(false);
    }
}