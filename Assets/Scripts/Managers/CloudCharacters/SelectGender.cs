
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class SelectGender : MonoBehaviour
{
   
   public void _SelectGender(string gender) 
    {
        System.Threading.Tasks.Task task = CloudCharacterManager.Instance.GetCloudAvatarData(gender);
    }
}
