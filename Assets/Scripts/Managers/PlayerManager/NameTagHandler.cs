using FF;
using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NameTagHandler : NetworkBehaviour
{
    [FormerlySerializedAs("ingameUsername")] public TextMeshProUGUI inGameUsername;
    

    private void Start()
    {
        GetComponent<RPMPlayerManager>().onAvatarLoaded.AddListener(SetUserName);
    }

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    void SetUsernameOnHost(string username)
    {
        inGameUsername.text = username;
    }

    [ServerRpc(RequireOwnership = false)]
    void SetUsernameOnServer(string playerName) //will call the observer RPC to sync the username to other player
    {
        SetUsernameOnHost(playerName);
    }

    void SetUserName() //will be called on start to set the username of the player
    {
        if (IsOwner)
        {
            Debug.Log("Am Owner");
            
                SetUsernameOnServer(DataManager.Instance.userData.name);
            

            inGameUsername.transform.parent.gameObject.SetActive(false);
        }
    }
}