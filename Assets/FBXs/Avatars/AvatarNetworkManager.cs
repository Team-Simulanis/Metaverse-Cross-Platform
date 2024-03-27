using FF;
using FishNet.Component.Animating;
using FishNet.Component.Transforming;
using FishNet.Object;
using UnityEngine;

public class AvatarNetworkManager : NetworkBehaviour
{
    #region NetworkObjects

    private NetworkObject _networkObject;
    private NetworkTransform _networkTransform;
    private NetworkAnimator _networkAnimator;

    #endregion


    private RPMPlayerManager _rpmPlayerManager;

    private void Awake()
    {
        _rpmPlayerManager = GetComponent<RPMPlayerManager>();

        if (_rpmPlayerManager.playerType == PlayerType.Networked)
        {
            GetComponent<NetworkObject>().enabled = true;
            GetComponent<NetworkAnimator>().enabled = true;
            GetComponent<NetworkTransform>().enabled = true;
        }
        else
        {
            GetComponent<NetworkObject>().enabled = false;
            GetComponent<NetworkAnimator>().enabled = false;
            GetComponent<NetworkTransform>().enabled = false;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        if (DataManager.Instance != null)
        {
            _rpmPlayerManager.currentAvatarUrl =
                DataManager.Instance.userData.avatarDetails.avatarModelDownloadLink;
            _rpmPlayerManager.ChangeAvatarUrl();
        }
        else
        {
            _rpmPlayerManager.currentAvatarUrl ="https://models.readyplayer.me/64bfc617fffca9bd5d533218.glb";
            _rpmPlayerManager.ChangeAvatarUrl();
            Debug.LogError("DataManager is not initialized");
        }
    }


    [ServerRpc]
    public void SendAvatarUpdateRequestServer(string url)
    {
        Debug.Log("Server Received : " + url);
        UpdatePlayerAvatar(url);
    }


    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    private void UpdatePlayerAvatar(string url)
    {
        _rpmPlayerManager.StartLoadingAvatar(url);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetUsernameOnServer(string playerName,string profileImageURL) //will call the observer RPC to sync the username to other player
    {
        SetUsernameOnHost(playerName,profileImageURL);
    }

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    private void SetUsernameOnHost(string username,string profileImageURL)
    {
        GetComponent<NameTagHandler>().SetUsernameOnHost(username,profileImageURL);
    }

    #region Emotes RPC's

    [ServerRpc(RequireOwnership = false)]
    public void PlayReactionOnServer(string animationName)
    {
        PlayerReactionOnObserver(animationName);
        Debug.Log("play Animation on server");
    }

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    private void PlayerReactionOnObserver(string animationName)
    {
        GetComponent<PlayerReactionHandler>().React(animationName);
        Debug.Log("play Animation on observer");
    }

    #endregion
   
}