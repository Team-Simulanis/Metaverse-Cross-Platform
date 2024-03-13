using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : NetworkBehaviour
{
    [SerializeField] Camera MiniMapCamera;
    [SerializeField] Image MiniMapImage;
    [SerializeField] Image BigMapImage;
    [SerializeField] GameObject ThisPlayer;
    private void Start()
    {
        CheckOwner();
        changePlayerArrowColor();
    }
    void LateUpdate()
    {
        if (IsOwner)
        {
            if (ThisPlayer != null)
            {
                Vector3 newPosition = ThisPlayer.transform.position;
                newPosition.y = MiniMapCamera.transform.position.y;
                MiniMapCamera.transform.position = newPosition;
            }
            else
                Debug.Log("not a player");
        }
    }
    void changePlayerArrowColor()
    {
        if (IsOwner)
        {
            MiniMapImage.color = Color.red;
            BigMapImage.color = Color.red;
        }
        else
        {
            MiniMapImage.color = Color.white;
            BigMapImage.color = Color.white;
        }
    }

    void CheckOwner()
    {
        if(!IsOwner) 
        {
            ThisPlayer = this.gameObject;
            MiniMapCamera.gameObject.SetActive(false);
        }
    }

}
