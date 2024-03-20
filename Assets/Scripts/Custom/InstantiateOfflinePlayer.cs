using FF;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class InstantiateOfflinePlayer : MonoBehaviour
{
    public GameObject offlinePlayerPrefab;
    GameObject offlinePlayer;
    // Start is called before the first frame update

    [Button]
    public async void OfflinePlayerInstantiate()
    {
        offlinePlayer = Instantiate(offlinePlayerPrefab,this.transform.position,Quaternion.identity);
        await Task.Delay(2000);
        offlinePlayer.GetComponent<RPMPlayerManager>().StartLoadingAvatar(DataManager.Instance.userData.avatarDetails.avatarModelDownloadLink);
    }
}
