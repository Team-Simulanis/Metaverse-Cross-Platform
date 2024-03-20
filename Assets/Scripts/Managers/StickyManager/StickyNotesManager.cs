using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using TMPro;
using FishNet.Object;

public class StickyNotesManager : NetworkBehaviour
{
    public Transform notesTransform;
    public GameObject StickyNotesPrefab;
    [SerializeField] private TMP_InputField messageText ;
    public static StickyNotesManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void InstantiateNote()
    {
        string message = messageText.text;
        if (message == "")
        {
        }
        else
        {
            InstanceNotes(message, notesTransform.position);
            messageText.text = "";
        }
    }

    [ServerRpc(RequireOwnership =false)]
    void InstanceNotes(string message,Vector3 stickyNoteTransform)
    {
        SyncStickyNotesText(message,stickyNoteTransform);
    }

    [ObserversRpc(ExcludeOwner = false, BufferLast = false)]
    void SyncStickyNotesText(string text,Vector3 stickyNotePosition)
    {
         Debug.Log(text);
         Debug.Log(stickyNotePosition + "position");
         GameObject Obj = Instantiate(StickyNotesPrefab, stickyNotePosition, Quaternion.identity);
         Obj.GetComponent<messageTextHolder>().msgText.text = text;
    }

    int GetUniqueID()
    {
       return System.Guid.NewGuid().GetHashCode(); 
    }

    public void TakeOwner(Transform playerTransform)
    {
        notesTransform = playerTransform;
    }
}
