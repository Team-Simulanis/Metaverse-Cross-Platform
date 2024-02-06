using System.Threading.Tasks;
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

    public void ValidateEmailFormat(string value)
    {
        if (value == "") return;
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

    public async void TryLogin()
    {
        Signal.Send(StreamId.Login.LoggingIn);

        if (GameManager.Instance.accessData.gameMode == AccessData.GameMode.Development)
        {
            await Task.Delay(1000);
            if (GameManager.Instance.accessData.loginCondition == AccessData.LoginStatus.LoginFailed)
            {
                Signal.Send(StreamId.Login.LoginFailed);
                IncorrectPassword();
                return;
            }
            else
            {
                Signal.Send(StreamId.Login.Success);
                return;
            }
        }
        // Production code goes here
    }

    private void IncorrectPassword()
    {
        passwordErrorContainer.Show();
        passwordErrorText.text = "Incorrect password";
    }
}