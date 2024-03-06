using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGender : MonoBehaviour
{
   public void _SelectGender(string gender)
    {
        CloudCharacterManager.instance.GetCloudAvatarData(gender);
    }
}
