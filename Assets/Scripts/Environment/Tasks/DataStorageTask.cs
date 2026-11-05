using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorageTask", menuName = "Scriptable Objects/DataStorageTask")]
public class DataStorageTask : Task
{
    public Employee[] employeeArray;
    public BinarySearchTree BST_Employees;

    public override void Execute()
    {

    }
}
