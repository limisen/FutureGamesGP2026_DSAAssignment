using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorageTask", menuName = "Scriptable Objects/DataStorageTask")]
public class DataStorageTask : Task
{
    public Employee[] employees;

    public override void Execute()
    {

    }
}
