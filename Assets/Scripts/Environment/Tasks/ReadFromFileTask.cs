using Environment.Tasks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReadFromFile", menuName = "Scriptable Objects/ReadFromFile")]
public class ReadFromFile : Task
{
    List<string> Data = new List<string>();
    [SerializeField] TextAsset FileToRead;
    [SerializeField] Task TaskToPassDataTO;

    public override void Execute()
    {
        // "Read" the file
        Debug.Log($"Path: {FileToRead}");
        
        // Parse the data from the file

        // split it up by person
        foreach (string item in FileToRead.text.Split("},"))
        {
            Debug.Log("Item: " + item);
            Data.Add(item);
        }

        // pass it to the seperate Task...


        //Debug.Log(JsonUtility.FromJson<ScriptableObject>("Assets/Json/testEmployees"));
    }
}
