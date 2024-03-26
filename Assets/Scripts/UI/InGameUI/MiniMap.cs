using FishNet.Object;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniMap : NetworkBehaviour
{
    [FormerlySerializedAs("MiniMapCamera")] [SerializeField]
    private Camera miniMapCamera;

    [FormerlySerializedAs("MiniMapImage")] [SerializeField]
    private Image miniMapImage;

    [FormerlySerializedAs("BigMapImage")] [SerializeField]
    private Image bigMapImage;

    private void Start()
    {
        CheckOwner();
        ChangePlayerArrowColor();
    }

    private void ChangePlayerArrowColor()
    {
        if (IsOwner)
        {
            miniMapImage.color = Color.red;
            bigMapImage.color = Color.red;
        }
        else
        {
            miniMapImage.color = Color.white;
            bigMapImage.color = Color.white;
        }
    }

    private void CheckOwner()
    {
        if (IsOwner) return;
        miniMapCamera.gameObject.SetActive(false);
    }
}