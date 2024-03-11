using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : NetworkBehaviour
{
    [SerializeField] Camera MiniMapCamera;
    [SerializeField] Image MiniMapImage;

    private void Start()
    {
        changePlayerArrowColor();
    }
    void LateUpdate()
    {
        if (IsOwner)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = MiniMapCamera.transform.position.y;
            MiniMapCamera.transform.position = newPosition;
        }
    }
    void changePlayerArrowColor()
    {
        if (IsOwner)
        {
            MiniMapImage.color = Color.red;
        }
        else
        {
            MiniMapImage.color = Color.white;   
        }
    }

}
