using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DataToArrayTask", menuName = "Scriptable Objects/DataToArrayTask")]
public class DataToArrayTask : Task
{
    public override void Execute()
    {
        throw new System.NotImplementedException();
        
        // Take the passed data and add it to an array.
        foreach (var item in name)
        {
            // Array.Add(item)
        }
    }
}
