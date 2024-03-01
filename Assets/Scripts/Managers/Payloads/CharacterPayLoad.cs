[System.Serializable]
public class CharacterPayload
{
    public bool status;
    public CharacterData[] data;
}

[System.Serializable]
public class CharacterData
{
    public string name;
    public string thumbnail;
    public CharacterDataAssets[] assets;
}

[System.Serializable]
public class CharacterDataAssets
{
    public string url;
    public string type;
}