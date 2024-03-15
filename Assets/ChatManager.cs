using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
   public static ChatManager Instance;
   public ChatPlayerButton chatPlayerButtonPrefab;
   public GameObject chatPlayerButtonParent;

   public List<ChatPlayerButton> chatPlayerButtons = new();
   
   public void AddPlayer(string userName, int id)
   {
     var cp= Instantiate(chatPlayerButtonPrefab, chatPlayerButtonParent.transform);
     
     cp.playerName.text = userName;
     cp.id = id;
     
     chatPlayerButtons.Add(cp);
   }

   public void RemovePlayer(ChatPlayerButton chatPlayerButton)
   {
      chatPlayerButtons.Remove(chatPlayerButton);
      Destroy(chatPlayerButton.gameObject);
   }
}
