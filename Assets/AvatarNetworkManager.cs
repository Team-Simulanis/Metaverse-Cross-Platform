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
}