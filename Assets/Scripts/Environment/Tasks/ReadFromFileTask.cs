using Environment.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReadFromFile", menuName = "Scriptable Objects/ReadFromFile")]
public class ReadFromFile : Task
{
    List<string> Data = new List<string>();
    [SerializeField] TextAsset FileToRead;
    [SerializeField] DataStorageTask TaskToPassDataTO;

    public override void Execute()
    {
        if (FileToRead == null) return;

        // "Read" the file
        Debug.Log($"Path: {FileToRead}");

        Employee[] Data = JsonUtility.FromJson<EmployeeArray>(FileToRead.text)?.employees;
        // Parse the data from the file

        foreach (var item in Data)
        {
            Debug.Log("EmployeeName: " + item.name);
            Debug.Log("EmployeeId: " + item.employeeId);
            Debug.Log("hasEngineRoomClearance: " + item.hasEngineRoomClearance);
            Debug.Log("lastEntranceInEngineRoom: " + item.lastEntranceInEngineRoom);
            item.lastEntranceInEngineRoomDate = DateTime.Parse(item.lastEntranceInEngineRoom);
            Debug.Log("lastEntranceInEngineRoomDate: " + item.lastEntranceInEngineRoomDate);
            Debug.Log("lastExitFromEngineRoom: " + item.lastExitFromEngineRoom);
        }
        TaskToPassDataTO.employees = Data;
        // pass it to the seperate Task...
    }
}
