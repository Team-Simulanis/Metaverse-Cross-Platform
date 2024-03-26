using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AccessData", menuName = "ScriptableObjects/Access Data", order = 1)]
public class AccessData : ScriptableObject
{
    [EnumToggleButtons][HideLabel][BoxGroup("Application Mode",CenterLabel = true)]
    public GameMode gameMode;

    [ShowIf("gameMode",GameMode.Development)][EnumToggleButtons][HideLabel][BoxGroup("Test Type",CenterLabel = true)]
    public Test testType;
    
    [ShowIf("testType",Test.Login)][EnumToggleButtons][HideLabel][BoxGroup("Login Condition",CenterLabel = true)]
    public LoginStatus loginCondition;
    public enum GameMode
    {
        Development,
        Production,
    }
    
    public Platform platform;

    public enum LoginStatus
    {
        LoginSuccess,
        LoginFailed
    }
    
    public enum Test
    {
        Login,
        NameTag
    }
    
    public enum Platform
    {
        Android,
        IOS,
        Windows,
        Mac    
    }
}
