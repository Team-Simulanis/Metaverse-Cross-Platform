using System.Collections;
using System.Collections.Generic;
using FF;
using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.UI;

public class AvatarButtonDownloader : MonoBehaviour
{
    [SerializeField] private string url = "https://cdn.simulanis.io/sso/uno/production/resources/9378a98f-e63d-4c20-8dce-180806c1def9/3DAssets/universal/2/M2.glb";
    [SerializeField] private AvatarRenderSettings renderSettings;
    [SerializeField] private Image image;
    
    private void Start()
    {
        return;
        var avatarRenderLoader = new AvatarRenderLoader();
        avatarRenderLoader.OnCompleted = SetImage;
        avatarRenderLoader.LoadRender(url, renderSettings);
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            DataManager.Instance.UpdateAvatarInfo(new AvatarDetails()
            {
                avatarModelDownloadLink = url
            });
        });
    }

    private void SetImage(Texture2D texture)
    {
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        image.sprite = sprite;
        image.preserveAspect = true;
        image.color = Color.white;
    }
}


