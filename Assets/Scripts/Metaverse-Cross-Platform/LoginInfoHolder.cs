using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _EmailPassword
{
    public string Email;
    public string Password;
}
[CreateAssetMenu(fileName = "LoginInfoHolder")]
public class LoginInfoHolder : ScriptableObject
{
    public GameObject Player;
    public _EmailPassword[] EmailPassword;

}
