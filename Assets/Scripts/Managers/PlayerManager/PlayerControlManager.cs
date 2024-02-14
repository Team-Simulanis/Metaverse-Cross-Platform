using System.Collections;
using System.Collections.Generic;
using FishNet.Example.Scened;
using FishNet.Object;
using UnityEngine;
using Invector.vCharacterController;
using Invector.vCamera;
using Invector;

public class PlayerControlManager : NetworkBehaviour
{
    public static PlayerControlManager _Instance;
   
    public vThirdPersonCameraListData CL;
   
   
    bool firstView;

    void Awake()
    {
        if (_Instance == null)
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
        if (IsOwner)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            firstView = !firstView;
            ChangeToFirstView(firstView);
        }
    }

    void ChangeToFirstView(bool value)
    {
        if (value)
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