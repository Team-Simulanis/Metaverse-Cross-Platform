using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using Invector.vCamera;
using Invector.vCharacterController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
public class Switchplayers : MonoBehaviour
{
    public vThirdPersonController controller;
    public vThirdPersonInput input;
    public vThirdPersonCamera thirdPersonCamera;
    [SerializeField] Transform vrPlayer;
    [SerializeField] bool move;
    //NetworkManager networkManager;
/*    public enum platform {Desktop,VR,Mobile};

    public  platform _platform;*/
    private void OnEnable()
    {
       controller = GetComponent<vThirdPersonController>();
       input = GetComponent<vThirdPersonInput>();
    }

    private void Update()
    {
        vrPlayer = sendTransform.thisTransform;
        if (move) 
        {
            movetoposition(vrPlayer);
            move = false;
        }
    }

    void movetoposition(Transform _object)
    {
        thirdPersonCamera.CameraMovement(false);
        input.lockInput = true;
        controller.MoveToPosition(_object);
    }




}
