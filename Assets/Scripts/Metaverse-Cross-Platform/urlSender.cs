using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class urlSender : MonoBehaviour
{
    public Image AvatarImage;
    public Image thisButtonImage;
    public string UrlForCharacter;

    private void Awake()
    {
        
    }
    private void Start()
    {
        AvatarImage = AvatarSelected.StaticAvatarImage;
    }
    private void OnEnable()
    {
       
    }

    public void ChangeImage()
    {
        AvatarImage.sprite = thisButtonImage.sprite;
        AvatarImage.color = thisButtonImage.color;
        ThirdPersonLoader.urlChanger(UrlForCharacter);

    }
}

