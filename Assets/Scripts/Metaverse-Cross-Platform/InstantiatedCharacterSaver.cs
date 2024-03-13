using System;
using FF;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Samples.LegacyAvatarCreator;
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class InstantiatedCharacterSaver : MonoBehaviour
{
    [SerializeField] AvatarCreatorStateMachine avatarCreatorStateMachine;
    [SerializeField] AvatarCreatorData avatarCreatorData;
    [SerializeField] LoginInfoHolder loginInfoHolder;
    [SerializeField] GameObject Player;
    [SerializeField] string playerID;
    [SerializeField] bool saveplayer;

    [SerializeField] bool LoadPlayer;

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
        if (saveplayer)
        {
            SavePlayer();
            saveplayer = false;
        }

        if (LoadPlayer)
        {
            loadPlayerInScene();
            LoadPlayer = false;
        }
    }

    [DrawButton]
    public void SavePlayer()
    {
        Debug.Log("Saving PlayerData");
        playerID = avatarCreatorData.AvatarProperties.Id;
        Player = GameObject.Find(playerID);


        loginInfoHolder.Player = Player;
        ThirdPersonLoader.urlChanger(playerID);
    }

    public void SaveCustomPlayer(string id)
    {
        Debug.Log("Saving PlayerData");
        DataManager.Instance.UpdateAvatarInfo(new AvatarDetails()
        {
            avatarModelDownloadLink = $"{Env.RPM_MODELS_BASE_URL}/{id}.glb"
        });
    }

    public void loadPlayerInScene()
    {
        if (loginInfoHolder.Player != null)
        {
            Instantiate(Player);
        }

        else
        {
        }
    }
}