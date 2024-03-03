using FF;
using FishNet.Object;
using TMPro;
using UnityEngine;
public class NameTagHandler : NetworkBehaviour
{
    public TextMeshProUGUI ingameUsername;
    public string _name;
    [SerializeField] bool takeNameFromaDatamanager; //when on takes the nametag from Datamanger

    private void OnEnable()
    {
        if (takeNameFromaDatamanager) { _name = DataManager.Instance.name; }
        setName();
    }
    [ObserversRpc]
    void showUsername(string username)
    {
        ingameUsername.text = username;
    }

    [ServerRpc(RequireOwnership = false)]
    void setUsername() //will call the observer RPC to sync the username to other player
    {
        showUsername(_name);
    }

    void setName() //will be called on start to st the username of the player
    {
        setUsername();
    }
}
