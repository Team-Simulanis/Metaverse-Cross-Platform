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
    public string NotesText;
    public bool instanceNotes;
    [SerializeField] private TMP_InputField messageText ;
    public static StickyNotesManager _instance;
    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    void Update()
    {
        if (instanceNotes)
        {
            InstanceNotes("hello");
            instanceNotes = false;
        }
    }

    public void InstantiateNote()
    {
        if(base.IsOwner) 
        {
            string message = messageText.text;
            InstanceNotes(message);
        }
    }

    [ServerRpc(RequireOwnership =false)]
    void InstanceNotes(string message)
    {
            SyncStickyNotesText(message);
    }

    [ObserversRpc(ExcludeOwner = true, BufferLast = false)]
    void SyncStickyNotesText(string text)
    {
        Debug.Log(text);
        GameObject Obj = Instantiate(StickyNotesPrefab, notesTransform.position, Quaternion.identity);
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
