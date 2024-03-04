using FishNet.Object;
using UnityEngine;

public class playerReactionHandler : NetworkBehaviour
{
    public static playerReactionHandler Instance;
    private void Start()
    {
        Instance = this;
    }
    public void PlayReactions(string animationName)
    {
        PlayReactionOnServer(animationName);
    }

    [ServerRpc(RequireOwnership = false)]
    public void PlayReactionOnServer(string animationName)
    {
        PlayeReactionOnObserver(animationName);
    }

    [ObserversRpc]
    public void PlayeReactionOnObserver(string animationName)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(animationName);
    }
}
