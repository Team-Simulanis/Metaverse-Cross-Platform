
using Invector.vCharacterController;
using UnityEngine;


public class AvatarInitializer : MonoBehaviour
{
    private bool _isMale;
    private Animator _animator;
    [SerializeField] private Animator maleAnimator;
    [SerializeField] private Animator femaleAnimator;
    
    public void SetupAvatar(GameObject targetAvatar, GameObject avatarController, GameObject avatar,bool isNetworkObject,bool isMale, GameObject invectorControl, Vector3 avatarPositionOffset)
    {
        _isMale = isMale;
        
        Debug.Log("Setting up "+ targetAvatar.name + " as Avatar");
        
        if (avatar != null)
        {
            avatarController.GetComponent<vThirdPersonController>().enabled = false;
            avatarController.GetComponent<vThirdPersonInput>().enabled = false;
            Destroy(avatar);
        }
        else
        {
            avatarController = invectorControl;
            avatarController.SetActive(true);
            _animator = avatarController.GetComponent<Animator>();
        }

        avatar = targetAvatar;
        avatar.transform.parent = avatarController.transform.GetChild(0);
        avatar.transform.localPosition = avatarPositionOffset;
        avatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DestroyImmediate(avatar.GetComponent<Animator>());
        if (!isNetworkObject)
        {
            avatarController.GetComponent<vThirdPersonController>().enabled = true;
            avatarController.GetComponent<vThirdPersonInput>().enabled = true;
            avatarController.transform.GetChild(3).gameObject.SetActive(true);
        }

        avatarController.SetActive(true);
       
    }

    public void ChangeAvatarRef()
    {
        if (_isMale)
        {
            Debug.Log("Assigned Male Avatar");
            var avatar = maleAnimator.avatar;
            _animator.avatar = avatar;
        }
        else
        {
            Debug.Log("Assigned Female Avatar");

            var avatar = femaleAnimator.avatar;
            _animator.avatar = avatar;
        }

        _animator.Rebind();
        _animator.Update(0f);
    }

}
