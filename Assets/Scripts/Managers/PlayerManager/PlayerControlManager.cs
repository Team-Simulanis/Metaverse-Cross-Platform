using System.Collections;
using System.Collections.Generic;
using FishNet.Example.Scened;
using UnityEngine;
using Invector.vCharacterController;
using Invector.vCamera;
using UnityEditor.Rendering.LookDev;
using Invector;

public class PlayerControlManager : MonoBehaviour
{
    public static PlayerControlManager _Instance;
    public vThirdPersonController thirdPersonController;
    public vThirdPersonCamera thirdPersonCamera;
    public vThirdPersonCameraListData CL;
    public GameObject _mainPlayer;
    bool firstView;
    void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        // Multiplayer Code for isMine
        if(!_mainPlayer)
        {
            thirdPersonController = FindObjectOfType<vThirdPersonController>(true);
            thirdPersonCamera = FindObjectOfType<vThirdPersonCamera>(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Multiplayer Code for isMine
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            firstView = !firstView;
            ChangeToFirstView(firstView);
        }
    }
    void ChangeToFirstView(bool value)
    {
        if(value)
        {
            // Need to Rewrite this wrt all camera states
            CL.tpCameraStates[0].defaultDistance = 0;
            CL.tpCameraStates[0].height = 1.8f;

            // Need to disable all mesh renderer
        }
        else
        {
            CL.tpCameraStates[0].defaultDistance = 2.5f;
            CL.tpCameraStates[0].height = 1.25f;
        }
    }
}
