using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoginPanel : MonoBehaviour
{
    [Required] [SerializeField] private UIContainer emailErrorContainer;
    [Required] [SerializeField] private TMP_Text emailErrorText;

    [Required] [SerializeField] private UIContainer passwordErrorContainer;
    [Required] [SerializeField] private TMP_Text passwordErrorText;

    [ReadOnly] public string enteredEmailId;
    [ReadOnly] public string enteredPassword;

    public  void ValidateEmailFormat(string value)
    {
        if(value == "")return;
        if (InputFieldValidator.ValidateEmail(value))
        {
            enteredEmailId = value;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            emailErrorContainer.Show();
            emailErrorText.text = "Please use a valid email address";
        }
    }

    protected void EnterPassword(string value)
    {
        enteredPassword = value;
    }

    public void TryLogin()
    {
        Signal.Send(StreamId.Login.LoggingIn);
        
        if(GameManager.Instance.gameMode == GameMode.AuthFree)
        {
            IncorrectPassword();
        }
    }

    private void IncorrectPassword()
    {
        passwordErrorContainer.Show();
        passwordErrorText.text = "Incorrect password";
    }
}