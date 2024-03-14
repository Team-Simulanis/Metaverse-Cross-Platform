using FishNet.Component.Animating;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerReactionHandler : NetworkBehaviour
{
    public static PlayerReactionHandler Instance;
    public bool owner;
    private void Start()
    {
        Instance = this;
        SendOwner();
    }
    [Button]
    private void SendOwner()
    {
        if (!IsOwner) return;
        EmojiPanel.instance.TakeOwner(this.gameObject);
        LocationsPanel.instance.TakeOwner(this.gameObject);
    }
    public void PlayReactions(string animationName)
    {
          PlayReactionOnServer(animationName);
          Debug.Log("play Animation");
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void PlayReactionOnServer(string animationName)
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
    }

}
