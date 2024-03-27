using FishNet;
using Simulanis.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiPanel : MonoBehaviour
{
    public GameObject ownerObject;
    //playerReactionHandler playerReactionHandler = new playerReactionHandler();
    public static EmojiPanel instance;

    private void Awake()
    {
        instance = this;
    }
    public void PlayReaction(string reactionName) //will be callled on button , the string will contain the name of the trigger which will be played.
    {
        ownerObject.GetComponent<PlayerReactionHandler>().PlayReactions(reactionName);
    }
    public void TakeOwner(GameObject OwnerObject)
    {
        ownerObject = OwnerObject;
    }
}
