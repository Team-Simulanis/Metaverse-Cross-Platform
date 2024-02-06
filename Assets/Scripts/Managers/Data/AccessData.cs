using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AccessData", menuName = "ScriptableObjects/Access Data", order = 1)]
public class AccessData : ScriptableObject
{
    [EnumToggleButtons][HideLabel][BoxGroup("Application Mode",CenterLabel = true)]
    public GameMode gameMode;

    [ShowIf("gameMode",AccessData.GameMode.Development)][EnumToggleButtons][HideLabel][BoxGroup("Login Condition",CenterLabel = true)]
    public LoginStatus loginCondition;
    public enum GameMode
    {
        Development,
        Production,
    }

    public enum LoginStatus
    {
        LoginSuccess,
        LoginFailed
    }
}
