using System.Collections.Generic;

[System.Serializable]
public class RoomData
{

    public int chapter;
    public string description;
    public string investigationDescription;
    public string effectTriggerName;
    public List<Exit> exits;
}