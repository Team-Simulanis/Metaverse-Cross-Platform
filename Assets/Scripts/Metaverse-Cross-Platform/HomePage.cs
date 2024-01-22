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
    public TMP_InputField Email; //Will contain The TMP for Homepage/Email field reference
    public UIContainer EmailErrorMessage; 
    public TMP_InputField Password; //Will contain The TMP for Password field reference
    public UIContainer PasswordErrorMessage; //Will contain The for passwordErrorMesage field reference 
    public Image HideUnhide; // Will contain the Image reference for the Icon that'll Hide and unhide the password 
    public Sprite Hide; // Sprite when the Password is hidden
    public Sprite Unhide; // Sprite when password is not Hidden
    [HideInInspector] public bool Hidden; // Will check if the password is hidden or not hidden
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

    public void _Validate() // will check if the password or email is Correct or Wrong
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
                if (LoginPage.Email.text.ToString() != _LoginInfoholder.EmailPassword[i].Email)
                {
                    LoginPage.EmailErrorMessage.Show();
                }
                if (LoginPage.Password.text.ToString() != _LoginInfoholder.EmailPassword[i].Password)
                {
                    LoginPage.PasswordErrorMessage.Show();
                }
                LoginPage.LoginButton.interactable = false;
            }
            
        }
    }

    public void HideUnhidePassword() //Will hide and Unhide the password
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
