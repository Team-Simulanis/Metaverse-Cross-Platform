using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable] [LabelText("User Data", SdfIconType.Person)]
public class UserData
{
    [ReadOnly][BoxGroup("Basic Info")] public string email = "email@default.com";
    [ReadOnly][BoxGroup("Basic Info")]public string name = "Default Name";
    [ReadOnly][BoxGroup("Basic Info")]public string designation = "Default Designation";
    [ReadOnly][BoxGroup("Basic Info")]public string experience = "5";
    [ReadOnly][BoxGroup("Basic Info")]public string bio = "Default Bio";
    [ReadOnly][BoxGroup("Basic Info")]public Sprite profileImage;
    [ReadOnly][BoxGroup("Basic Info")]public string profileImageUrl;
    public AvatarDetails avatarDetails;
    public UserPermission userPermission;
}

[Serializable][LabelText("Avatar Info", SdfIconType.PersonBadge)]
public class AvatarDetails
{
    public string avatarName;
    public string avatarImageDownloadLink;
    public string avatarModelDownloadLink;
}

[Serializable]
public enum UserPermission
{
    Admin,
    User
}