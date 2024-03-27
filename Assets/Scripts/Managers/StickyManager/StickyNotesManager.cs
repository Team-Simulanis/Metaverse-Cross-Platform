using UnityEngine;
using FishNet;
using TMPro;
using FishNet.Object;

public class StickyNotesManager : NetworkBehaviour
{
    public Transform notesTransform;
    public Transform flagTramsform;
    public GameObject stickyNotesPrefab;
    public GameObject flagPrefab;
    [SerializeField] private TMP_InputField messageText ;
    public static StickyNotesManager instance;
    void Awake()
    {
         instance = this;
    }

    public void InstantiateNote(bool isFlag) //off when spawning sticky notes , On When spawning Flag
    {
        string message = messageText.text;
        if (!isFlag)
        {
            if (message == "")
            {
            }
            else
            {
                InstanceNotes(message, notesTransform.position, isFlag);
                messageText.text = "";
            }
        }

        else if (isFlag)
        {
            InstanceNotes(message, flagTramsform.position, isFlag);
        }

    }

    [ServerRpc(RequireOwnership =false)]
    void InstanceNotes(string message,Vector3 stickyNoteTransform, bool isNotFlag)
    {
        SyncStickyNotesText(message,stickyNoteTransform,isNotFlag);
    }

    [ObserversRpc(ExcludeOwner = false, BufferLast = false)]
    void SyncStickyNotesText(string text,Vector3 stickyNotePosition,bool isNotFlag)
    {
        if (!isNotFlag) 
        {
            Debug.Log(text);
            GameObject Obj = Instantiate(stickyNotesPrefab, stickyNotePosition, Quaternion.identity);
            Obj.GetComponent<messageTextHolder>().msgText.text = text;
        }

        else if (isNotFlag)
        {
            GameObject obj = Instantiate(flagPrefab,stickyNotePosition, Quaternion.identity);
        }

    }

    int GetUniqueID()
    {
       return System.Guid.NewGuid().GetHashCode(); 
    }

    public void TakeOwner(Transform playerTransform,Transform flagTransform)
    {
        notesTransform = playerTransform;
        flagTramsform = flagTransform;
        Debug.Log("ticky note location acquired"+notesTransform.position);
    }
}
