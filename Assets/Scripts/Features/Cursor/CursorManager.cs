
using UnityEngine;
using Invector.vCharacterController;
using Invector.vCamera;
using Invector;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CursorManager : MonoBehaviour
{
    bool _isCursorLocked;
    vThirdPersonController _vThirdPersonController;
    public vThirdPersonCamera playerCamera;
    [FormerlySerializedAs("StopPlayer")] public bool stopPlayer;
    bool _firstView;
    float _speed;
    private Renderer[] _renderers;
    public static CursorManager instance;
    [FormerlySerializedAs("CL")] public vThirdPersonCameraListData cl;
    private static readonly int InputMagnitude = Animator.StringToHash("InputMagnitude");

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _vThirdPersonController = GetComponent<vThirdPersonController>();
        _speed = _vThirdPersonController.speedMultiplier;
        ChangeToFirstView(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("middle button pressed");
            _isCursorLocked = !_isCursorLocked;
            SetCursorLocked(_isCursorLocked);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            _firstView = !_firstView;
            ChangeToFirstView(_firstView);
        }

        if(_isCursorLocked) 
        {
            if(Mouse.current.rightButton.wasPressedThisFrame)
            {
                this.gameObject.GetComponent<LaserPointer>().DrawRay(true);
            }

            else if(Mouse.current.rightButton.wasReleasedThisFrame) 
            {
                this.gameObject.GetComponent<LaserPointer>().DrawRay(false);
            }
        }

        else if(!_isCursorLocked)
        {
            this.gameObject.GetComponent<LaserPointer>().DrawRay(false);
        }
    }

    public void SetCursorLocked(bool value)
    {
        Cursor.visible = value;
        playerCamera.isFreezed = value;
        GetComponent<vThirdPersonInput>().lockInput = value;
        GetComponent<vThirdPersonController>().lockAnimMovement = !value;
        GetComponent<vThirdPersonAnimator>().disableAnimations = value;
        stopPlayer = value;
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            _vThirdPersonController.speedMultiplier = 0;
            GetComponent<Animator>().SetFloat(InputMagnitude, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            _vThirdPersonController.speedMultiplier = _speed;
        }
    }

    void ChangeToFirstView(bool value)
    {
        if (value)
        {
            cl.tpCameraStates[0].defaultDistance = 0;
            cl.tpCameraStates[0].height = 1.8f;
        }
        else
        {
            cl.tpCameraStates[0].defaultDistance = 2.5f;
            cl.tpCameraStates[0].height = 1.25f;
        }

        ChangePlayerRenderer(value);
    }

    void ChangePlayerRenderer(bool value)
    {
        _renderers = transform.GetComponentsInChildren<Renderer>();
        foreach (var r in _renderers)
        {
            r.enabled = !value;
        }
    }
}