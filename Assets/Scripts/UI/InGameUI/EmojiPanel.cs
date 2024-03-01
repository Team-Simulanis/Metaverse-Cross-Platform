using Simulanis.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playReaction(string reactionName)
    {
        PlayerControlManager._Instance.playReactions(reactionName);
    }
    public void clapEmoji() //will be called when clicked on clap Emoji
    {

    }

    public void thumbsUpEmoji() //will be called when clicked on ThumbsUp Emoji
    {

    }

    public void raiseHandEmoji() //will be called when clicked on Raise hand Emoji
    {

    }

    public void victoryEmoji() //will be called when clicked on victory Emoji
    {

    }

    public void waveEmoji() //will be called when clicked on Wave Emoji
    {

    }

    public void goodEmoji() //will be called when clicked on good Emoji
    {

    }
}
