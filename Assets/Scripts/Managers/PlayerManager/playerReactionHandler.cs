using FishNet.Component.Animating;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;

public class playerReactionHandler : NetworkBehaviour
{
    public static playerReactionHandler instance;
    public bool owner;
    private void Start()
    {
        instance = this;
        SendOwner();
    }
    [Button]
    private void SendOwner()
    {
        if (base.IsOwner)
        {
            EmojiPanel.instance.TakeOwner(this.gameObject);
            LocationsPanel.instance.TakeOwner(this.gameObject); //
        }
    }
    public void PlayReactions(string animationName)
    {
          PlayReactionOnServer(animationName);
          Debug.Log("play Animation");
    }
    [ServerRpc(RequireOwnership = false)]
    public void PlayReactionOnServer(string animationName)
    {
        PlayeReactionOnObserver(animationName);
        Debug.Log("play Animation on server");
    }

    [ObserversRpc(BufferLast = true, ExcludeOwner = false, RunLocally = true)]
    public void PlayeReactionOnObserver(string animationName)
    {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger(animationName);
            Debug.Log("play Animation on observer");
            //this.GetComponent<NetworkAnimator>().Play(animationName);
            
    }

}
