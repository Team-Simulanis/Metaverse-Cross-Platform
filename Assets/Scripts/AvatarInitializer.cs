using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Invector.vCharacterController;
using Sirenix.OdinInspector;
using UnityEngine;

public class AvatarInitializer : MonoBehaviour
{
    private bool _isMale;
    private Animator _animator;
    [SerializeField] private Animator maleAnimator;
    [SerializeField] private Animator femaleAnimator;
    public bool avatarSetupInProgress;

    private vThirdPersonController _invectorControlThirdPersonController;
    private vThirdPersonInput _invectorControlThirdPersonInput;

    private AvatarBodyType _avatarBodyType;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _invectorControlThirdPersonInput = GetComponent<vThirdPersonInput>();
        _invectorControlThirdPersonController = GetComponent<vThirdPersonController>();
    }

    public async Task<GameObject> SetupAvatar(GameObject targetAvatar, GameObject avatar, AvatarBodyType avatarBodyType,
        Vector3 avatarPositionOffset)
    {
        while (avatarSetupInProgress)
        {
            Debug.Log("Setup is busy");
            await Task.Delay(500);
        }

        return await SetupAvatarQ(targetAvatar, avatar, avatarBodyType,
            avatarPositionOffset);
    }

    private async Task<GameObject> SetupAvatarQ(GameObject targetAvatar, GameObject avatar,
        AvatarBodyType avatarBodyType, Vector3 avatarPositionOffset)
    {
        _avatarBodyType = avatarBodyType;
        avatarSetupInProgress = true;

        Debug.Log("Setting up " + targetAvatar.name + " as Avatar");

        if (avatar != null)
        {
            Debug.Log("Old Avatar Found");
            _invectorControlThirdPersonController.enabled = false;
            _invectorControlThirdPersonInput.enabled = false;

            Destroy(avatar);
        }
        else
        {
            Debug.Log("No Old Avatar Found");
        }

        avatar = targetAvatar;
        avatar.transform.parent = transform.GetChild(0);
        avatar.transform.localPosition = avatarPositionOffset;
        avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DestroyImmediate(avatar.GetComponent<Animator>());

#if UNITY_ANDROID
            _invectorControlThirdPersonInput.unlockCursorOnStart = true;
            _invectorControlThirdPersonInput.showCursorOnStart = true;
#endif
        _invectorControlThirdPersonController.enabled = true;
        _invectorControlThirdPersonInput.enabled = true;
        transform.GetChild(3).gameObject.SetActive(true);


        ChangeAvatarRef();

        await UniTask.DelayFrame(1);
        AnimatorRebind();

        return targetAvatar;
    }

    private void ChangeAvatarRef()
    {
        if (_avatarBodyType == AvatarBodyType.Masculine)
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

    [Button]
    public void AnimatorRebind()
    {
        _animator.Rebind();
        _animator.Update(0f);
    }
}