
using FishNet.Object;
using UnityEngine;
using Invector.vCharacterController;
using Invector.vCamera;
using Invector;
using UnityEngine.InputSystem;
using FishNet.Connection;

namespace Simulanis.Player
{
    //
    public class PlayerControlManager : NetworkBehaviour
    {
        public static PlayerControlManager _Instance;
        public vThirdPersonCameraListData CL;
        public vThirdPersonCamera playerCamera;
        private Renderer[] renderers;
        public int userId;

        public bool StopPlayer = false;

        bool firstView;
        bool isCursorLocked;
        float speed;

        vThirdPersonController vThirdPersonController;

        void Awake()
        {
            userId = this.GetInstanceID();
            if (_Instance == null)
            {
                _Instance = this;
            }
            vThirdPersonController = GetComponent<vThirdPersonController>();
            speed = vThirdPersonController.speedMultiplier;
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            ChangeToFirstView(false);
            sendInfo();
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
            if (Mouse.current.middleButton.wasPressedThisFrame)
            {
                Debug.Log("middle button pressed");
                isCursorLocked = !isCursorLocked;
                SetCursorLocked(isCursorLocked);
            }

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
                CL.tpCameraStates[0].defaultDistance = 0;
                CL.tpCameraStates[0].height = 1.8f;
            }
            else
            {
                CL.tpCameraStates[0].defaultDistance = 2.5f;
                CL.tpCameraStates[0].height = 1.25f;
            }
            ChangePlayerRenderer(value);
        }
        //to lock the player movement animation whenever some button is pressed 
        void SetCursorLocked(bool value) 
        {
            Cursor.visible = value;
            playerCamera.isFreezed = value;
            GetComponent<vThirdPersonInput>().lockInput = value;
            GetComponent<vThirdPersonController>().lockAnimMovement = !value;
            GetComponent<vThirdPersonAnimator>().disableAnimations = value;
            StopPlayer = true;
            if (value)
            {
                Cursor.lockState = CursorLockMode.None;
                vThirdPersonController.speedMultiplier = 0;   
                GetComponent<Animator>().SetFloat("InputMagnitude", 0);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                vThirdPersonController.speedMultiplier = speed;
            }

        }
        void ChangePlayerRenderer(bool value)
        {
            renderers = transform.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                r.enabled = !value;
            }
        }
        void sendInfo() // called on start , it add this player to the list whener this player is spawned
        {
            listplayerinfo.instance.addNewPlayer(this.OwnerId,this.name);
        }


        private void OnDestroy() //whenever this player is despawnes it removes itselfs from the player list 
        {
            listplayerinfo.instance.removeNewPlayer(this.OwnerId, this.name);
            Debug.Log("destroyed");
        }
    }
}