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
        if(_instance == null)
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
        if(instanceNotes) 
        {
            InstanceNotes();
            instanceNotes = false;
        }
    }
    void InstanceNotes()
    {
        //int noteId = GetUniqueID();
        SyncStickyNotesText(NotesText,notesTransform.position.x,notesTransform.position.y,notesTransform.position.z);
        //SyncStickyNotesPostion(noteId, notesTransform.position.x,notesTransform.position.y,notesTransform.position.z);
    }
    [ObserversRpc(ExcludeOwner = true, BufferLast = false)]
    void SyncStickyNotesText(string text, float positionX, float positionY, float positionZ)
    {
        Debug.Log("positionX "+ positionX);
        Debug.Log("positionY "+ positionY);
        Debug.Log("position "+ positionZ);
        Debug.Log(text);
        GameObject Obj = Instantiate(StickyNotesPrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
        Obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = text;
       
    }
    int GetUniqueID()
    {
       return System.Guid.NewGuid().GetHashCode(); 
    }
}
