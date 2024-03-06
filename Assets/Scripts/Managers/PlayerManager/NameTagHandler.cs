using FishNet.Object;
using TMPro;
using UnityEngine;
public class NameTagHandler : NetworkBehaviour
{
    public TextMeshProUGUI ingameUsername;
    public string _name;
    [SerializeField] bool takeNameFromaDatamanager; //when on takes the nametag from Datamanger
   
    private void Start()
    {
        GetComponent<RPMPlayerManager>().onAvatarLoaded.AddListener(() =>
        {
            SetUserName();
        });
    }
    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    void SetUsernameOnHost(string username)
    {
        ingameUsername.text = username;
    }

    [ServerRpc (RequireOwnership =false)]
    void SetUsernameOnServer(string name) //will call the observer RPC to sync the username to other player
    {
        SetUsernameOnHost(name);
    }
    void SetUserName() //will be called on start to set the username of the player
    {
        if (base.IsOwner)
        { 
            Debug.Log("Am Owner");
            if(takeNameFromaDatamanager) {SetUsernameOnServer(GameManager.Instance.name); }
            else { SetUsernameOnServer(_name); }
            ingameUsername.transform.parent.gameObject.SetActive(false);
        }
    }
}
