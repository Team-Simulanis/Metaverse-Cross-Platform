using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[Serializable]
public class Data
{
    public string avatar;
    public string uuid;
    public string name;
    public string email;
    public object uid;
    public object phone;
    public string designation;
    public string bio;
    public MetaData metaData;
    public Group group;
    public List<string> permissions;
    public Role role;
    public string sessionId;
    public Branding branding;
}

[Serializable]
public class Group
{
    public string name;
    public string uuid;
    public string avatar;
}

[Serializable]
public class MetaData
{
}

[Serializable]
public class Role
{
    public string name;
    public string description;
    public string color;
}

[Serializable]
public class UserDetailsPayload
{
    public bool status;
    public Data data;
}

[Serializable]
public class Branding
{
    public string logo;
    public string name;
}