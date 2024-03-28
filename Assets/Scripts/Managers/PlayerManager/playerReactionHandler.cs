using Invector;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerReactionHandler : MonoBehaviour
{
    public static PlayerReactionHandler Instance;
    public bool owner;
    public Transform stickyNoteTransform;
    public Transform flagTransform;
    public LineRenderer ownerLineRenderer;
    public GameObject ownerPointer;
    private void Start()
    {
        SendOwner();
    }

    [Button]
    private void SendOwner()
    {
        if (GetComponent<RPMPlayerManager>().playerType == PlayerType.Networked)
        {
            if(GetComponent<AvatarNetworkManager>().IsOwner)
            {
                EmojiPanel.instance.TakeOwner(this.gameObject);
                LocationsPanel.instance.TakeOwner(this.gameObject);
                StickyNotesManager.instance.TakeOwner(stickyNoteTransform, flagTransform);
                DynamicArrows.instance.TakeOwner(this.gameObject.transform);
                LaserPointer.instance.TakeOwner(ownerLineRenderer);
                //LaserPointer.instance.TakeOwner(rayOriginPoint,mainCamera);
            }
        }
        else
        {
            EmojiPanel.instance.TakeOwner(gameObject);
            return;
        }
    }

    public void PlayReactions(string animationName)
    {
        if (GetComponent<RPMPlayerManager>().playerType == PlayerType.Networked)
        {
            GetComponent<AvatarNetworkManager>().PlayReactionOnServer(animationName);
        }
        else
        {
            React(animationName);
        }

        Debug.Log("play Animation");
    }

    private void Update()
    {
 
    }
    public void React(string key)
    {
        var animator = GetComponent<Animator>();
        animator.SetTrigger(key);
    }
}