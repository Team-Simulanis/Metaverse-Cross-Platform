using System.Collections;
using System.Collections.Generic;
using FishNet.Example.Scened;
using FishNet.Object;
using UnityEngine;
using Invector.vCharacterController;
using Invector.vCamera;
using Invector;
using UnityEngine.InputSystem;

public class PlayerControlManager : NetworkBehaviour
{
    public static PlayerControlManager _Instance;
   
    public vThirdPersonCameraListData CL;
    public vThirdPersonCamera playerCamera;
    private Renderer[] renderers;
   
   
    bool firstView;
    bool isCursorLocked;

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
        ChangeToFirstView(false);
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
        //if (!IsOwner) return;
        if (Mouse.current.middleButton.isPressed)
        {
            Debug.Log("middle button pressed");
            isCursorLocked = !isCursorLocked;
            SetCursorLocked(isCursorLocked);
        }
        
/*        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            firstView = !firstView;
            ChangeToFirstView(firstView);
        }*/
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L Pressed");
            isCursorLocked = !isCursorLocked;
            SetCursorLocked(isCursorLocked);
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
        ChangePlayerRenderer(value);
    }
    void SetCursorLocked(bool value)
    {
        Cursor.visible = value;
        playerCamera.isFreezed = value;
        //GetComponent<Rigidbody>().useGravity = !value;
        GetComponent<vThirdPersonInput>().lockMoveInput = value;
        if(value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void ChangePlayerRenderer(bool value)
    {
        renderers = transform.GetComponentsInChildren<Renderer>();
        foreach(var r in renderers)
        {
            r.enabled = !value;
        }
    }
}