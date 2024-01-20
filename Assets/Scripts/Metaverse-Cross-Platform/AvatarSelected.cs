using Doozy.Runtime.UIManager.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelected : MonoBehaviour
{

    public Image avatar; //will contain the image where the selected avatar is shown
    public UIButton[] buttonToSelectAvatar; // will hold the button to select the Avatar 
    public string[] avatarURL;
    public static Image StaticAvatarImage;

    private void start()
    {
        
    }
    private void OnEnable()
    {
        StaticAvatarImage = avatar;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
