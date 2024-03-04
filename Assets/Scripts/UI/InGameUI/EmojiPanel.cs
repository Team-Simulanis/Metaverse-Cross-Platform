using Simulanis.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiPanel : MonoBehaviour
{
    public void playReaction(string reactionName) //will be callled on button , the string will contain the name of the trigger which will be played.
    {
       playerReactionHandler.Instance.PlayReactions(reactionName);
    }
}
