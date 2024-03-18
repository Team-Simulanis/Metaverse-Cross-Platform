using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickyNoteController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    [Button]
    public void PutStickyNote()
    {
        string text = inputField.text;
        StickyNotesManager._instance.InstantiateNote(text);
    }
}
