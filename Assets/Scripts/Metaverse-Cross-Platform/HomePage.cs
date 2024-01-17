using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct _LoginPage
{
    public TMP_InputField Email;
    public UIContainer EmailErrorMessage;
    public TMP_InputField Password;
    public UIContainer PasswordErrorMessage;
    public Image HideUnhide;
    public Sprite Hide;
    public Sprite Unhide;
    [HideInInspector] public bool Hidden;
    public UIButton LoginButton;
}

[System.Serializable]
public struct _InfoPage
{
    public TMP_InputField Name;
    [HideInInspector]public bool NameFilled;
    public TMP_InputField Email;
    [HideInInspector]public bool EmailFilled;
    public TMP_InputField Designation;
    [HideInInspector] public bool DesignationFilled;
    public TMP_InputField Experience;
    [HideInInspector] public bool ExperienceFilled;
    public TMP_InputField Bio;
    [HideInInspector] public bool BioFilled;
    public Image BackImage;
    [Range(0,1)]
    public float Compleated;
     
}
public class HomePage : MonoBehaviour
{
    public LoginInfoHolder _LoginInfoholder;
    public _LoginPage LoginPage;
    public _InfoPage InfoPage;
    [SerializeField] string Message;
    // Start is called before the first frame update
    void Start()
    {
        LoginPage.Hidden = true;
        InfoPage.NameFilled = false;
        InfoPage.EmailFilled = false;
        InfoPage.DesignationFilled = false;
        InfoPage.ExperienceFilled = false;
        InfoPage.BioFilled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _Validate()
    {
        for (int i = 0; i < _LoginInfoholder.EmailPassword.Length; i++)
        {

            if (LoginPage.Email.text.ToString() ==_LoginInfoholder.EmailPassword[i].Email && LoginPage.Password.text.ToString() == _LoginInfoholder.EmailPassword[i].Password)
            {
                Message = "Correct";
                LoginPage.LoginButton.interactable = true;
            }

            else
            {
                Message = "Not Correct";
                LoginPage.PasswordErrorMessage.Show();
                LoginPage.EmailErrorMessage.Show();
                LoginPage.LoginButton.interactable = false;
            }
            
        }
    }

    public void HideUnhidePassword()
    {
        if (LoginPage.Hidden)
        {
            LoginPage.Password.contentType =TMP_InputField.ContentType.Standard;
            LoginPage.Password.ForceLabelUpdate();
            LoginPage.HideUnhide.sprite = LoginPage.Hide; 
            LoginPage.Hidden = false;
        }

        else if (!LoginPage.Hidden)
        {
            LoginPage.Password.contentType =TMP_InputField.ContentType.Password;
            LoginPage.Password.ForceLabelUpdate();
            LoginPage.HideUnhide.sprite = LoginPage.Unhide;
            LoginPage.Hidden = true;
        }
    }

    public void EnteredName()
    {
        if (!InfoPage.NameFilled)
        {
            if (InfoPage.Name.text != null || InfoPage.Name.text != "")
            {
                InfoPage.BackImage.fillAmount = +0.2f;
                InfoPage.Compleated = +0.2f;
                InfoPage.NameFilled = true;
            }
        }
        else if (InfoPage.NameFilled)
        {
            if (InfoPage.Name.text == null || InfoPage.Name.text == "")
            {
                InfoPage.BackImage.fillAmount = -0.2f;
                InfoPage.Compleated = -0.2f;
                InfoPage.NameFilled = false;
            }
        }
        
    }

    public void EnteredEmail()
    {

    }

    public void EnteredDesignation()
    {

    }

    public void EnteredExperience()
    {

    }
    public void EnteredBio()
    {

    }
}
