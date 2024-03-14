
using UnityEngine;

public class SelectGender : MonoBehaviour
{
   public void _SelectGender(string gender) 
    {
        CloudCharacterManager.Instance.GetCloudAvatarData(gender);
    }
}
