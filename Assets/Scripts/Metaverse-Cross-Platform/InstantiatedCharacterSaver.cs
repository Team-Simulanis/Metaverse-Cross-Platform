using FF;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Samples.LegacyAvatarCreator;
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;
using UnityEngine.Serialization;

public class InstantiatedCharacterSaver : MonoBehaviour
{
    [SerializeField] AvatarCreatorStateMachine avatarCreatorStateMachine;
    [SerializeField] AvatarCreatorData avatarCreatorData;
    [SerializeField] LoginInfoHolder loginInfoHolder;
    [FormerlySerializedAs("Player")] [SerializeField] GameObject player;
    [SerializeField] private string playerID;
    [FormerlySerializedAs("saveplayer")] [SerializeField] private bool savePlayer;

    [FormerlySerializedAs("LoadPlayer")] [SerializeField]
    private bool loadPlayer;

    // Start is called before the first frame update
    void OnEnable()
    {
        avatarCreatorStateMachine.AvatarSaved += SaveCustomPlayer;
    }

    private void OnDisable()
    {
        avatarCreatorStateMachine.AvatarSaved -= SaveCustomPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (savePlayer)
        {
            SavePlayer();
            savePlayer = false;
        }

        if (loadPlayer)
        {
            LoadPlayerInScene();
            loadPlayer = false;
        }
    }

    [DrawButton]
    private void SavePlayer()
    {
        Debug.Log("Saving PlayerData");
        playerID = avatarCreatorData.AvatarProperties.Id;
        player = GameObject.Find(playerID);


        loginInfoHolder.Player = player;
        ThirdPersonLoader.urlChanger(playerID);
    }

    private static void SaveCustomPlayer(string id)
    {
        Debug.Log("Saving PlayerData");
        DataManager.Instance.UpdateAvatarInfo(new AvatarDetails()
        {
            avatarModelDownloadLink = $"{Env.RPM_MODELS_BASE_URL}/{id}.glb"
        });
    }

    private void LoadPlayerInScene()
    {
        if (loginInfoHolder.Player != null)
        {
            Instantiate(player);
        }
    }
}