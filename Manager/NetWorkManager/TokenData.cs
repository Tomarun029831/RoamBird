using System.Collections.Generic;

[System.Serializable]
public class TokenData
{
    public TokenData(string token, Dictionary<uint, StageData> trackingDatas)
    {
        this.token = token;
        this.trackingDatas = trackingDatas;
    }
    public string token;
    public Dictionary<uint, StageData> trackingDatas;
}
