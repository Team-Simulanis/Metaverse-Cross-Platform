// using ReadyPlayerMe.Samples.LegacyAvatarCreator;
// using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedCharacterSaver : MonoBehaviour
{

   // [SerializeField] AvatarCreatorData avatarCreatorData;
    [SerializeField] LoginInfoHolder  loginInfoHolder;
    [SerializeField] GameObject Player;
    [SerializeField]string playerID;
    [SerializeField] bool saveplayer;
    [SerializeField] bool LoadPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
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
            LoadPlayer =false;
        }
    }
    [DrawButton]
    public void SavePlayer()
    {
      //  playerID = avatarCreatorData.AvatarProperties.Id;
        Player = GameObject.Find(playerID);
        loginInfoHolder.Player = Player;
     //   ThirdPersonLoader.urlChanger(playerID);
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
