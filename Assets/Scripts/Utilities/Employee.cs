using System;

[Serializable]
public class Employee
{
    public string name;
    public int employeeId;
    public bool hasEngineRoomClearance;
    public DateTime lastEntranceInEngineRoomDate;
    public string lastEntranceInEngineRoom;

    public string lastExitFromEngineRoom;
}
