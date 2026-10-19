using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DataToArrayTask", menuName = "Scriptable Objects/DataToArrayTask")]
public class DataToArrayTask : Task
{
    public Employee[] employees;

    public override void Execute()
    {
        
    }
}
