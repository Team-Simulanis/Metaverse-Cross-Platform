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
    GameObject N;
    public static StickyNotesManager _instance;
    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void AssignPlayerTransform(Transform T)
    {
        notesTransform = T;
    }
    // Update is called once per frame
    void Update()
    {
        if (instanceNotes)
        {
            //InstantiateNote();
            instanceNotes = false;
        }
    }

    public void InstantiateNote(string message)
    {
        if(base.IsOwner) 
        {
            InstanceNotes(message);
        }
    }
    [ServerRpc]
    void InstanceNotes(string message)
    {
        //int noteId = GetUniqueID();
            SyncStickyNotesText(message);
        //SyncStickyNotesPostion(noteId, notesTransform.position.x,notesTransform.position.y,notesTransform.position.z);
    }
    [ObserversRpc(ExcludeOwner = true, BufferLast = false)]
    void SyncStickyNotesText(string text)
    {
        //Debug.Log("positionX "+ positionX);
        //Debug.Log("positionY "+ positionY);
        //Debug.Log("position "+ positionZ);
        Debug.Log(text);
        GameObject Obj = Instantiate(StickyNotesPrefab, notesTransform.position, Quaternion.identity);
        Obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = text;
       
    }
    int GetUniqueID()
    {
       return System.Guid.NewGuid().GetHashCode(); 
    }
}
