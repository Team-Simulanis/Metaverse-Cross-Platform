using Doozy.Runtime.UIManager.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelected : MonoBehaviour
{

    public Image avatar; //will contain the image where the selected avatar is shown
    public static Image StaticAvatarImage;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        StaticAvatarImage = avatar;
    }

}
