using System.Threading.Tasks;
using Invector.vCharacterController;
using UnityEngine;
public class AvatarInitializer : MonoBehaviour
{
    private bool _isMale;
    private Animator _animator;
    [SerializeField] private Animator maleAnimator;
    [SerializeField] private Animator femaleAnimator;
    public bool avatarSetupInProgress;

    public async Task<GameObject> SetupAvatar(GameObject targetAvatar, GameObject avatar,
        bool isNetworkObject, bool isMale, GameObject invectorControl, Vector3 avatarPositionOffset)
    {
        while (avatarSetupInProgress)
        {
            Debug.Log("Setup is busy");
            await Task.Delay(500);
        }

        return SetupAvatarQ(targetAvatar, avatar, isNetworkObject, isMale, invectorControl,
            avatarPositionOffset);
    }

    private GameObject SetupAvatarQ(GameObject targetAvatar, GameObject avatar,
        bool isNetworkObject, bool isMale, GameObject invectorControl, Vector3 avatarPositionOffset)
    {
        avatarSetupInProgress = true;
        _isMale = isMale;

        Debug.Log("Setting up " + targetAvatar.name + " as Avatar");

        if (avatar != null)
        {
            Debug.Log("Old Avatar Found");
            invectorControl.GetComponent<vThirdPersonController>().enabled = false;
            invectorControl.GetComponent<vThirdPersonInput>().enabled = false;
            invectorControl.SetActive(true);
            _animator = invectorControl.GetComponent<Animator>();
            Destroy(avatar);
        }
        else
        {
            Debug.Log("No Old Avatar Found");
            invectorControl.SetActive(true);
            _animator = invectorControl.GetComponent<Animator>();
        }

        avatar = targetAvatar;
        avatar.transform.parent = invectorControl.transform.GetChild(0);
        avatar.transform.localPosition = avatarPositionOffset;
        avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DestroyImmediate(avatar.GetComponent<Animator>());
        if (!isNetworkObject)
        {
            invectorControl.GetComponent<vThirdPersonController>().enabled = true;
            invectorControl.GetComponent<vThirdPersonInput>().enabled = true;
            invectorControl.transform.GetChild(3).gameObject.SetActive(true);
        }

        invectorControl.SetActive(true);
        ChangeAvatarRef();
        return targetAvatar;
    }

    private void ChangeAvatarRef()
    {
        if (_isMale)
        {
            Debug.Log("Assigned Male Avatar");
            _animator.avatar = maleAnimator.avatar;
        }
        else
        {
            Debug.Log("Assigned Female Avatar");
            _animator.avatar = femaleAnimator.avatar;
        }

        _animator.Rebind();
        _animator.Update(0f);
        
        Debug.Log("Avatar Setup Complete");
        avatarSetupInProgress = false;
    }
}