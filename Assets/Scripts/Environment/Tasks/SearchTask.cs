using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchTask", menuName = "Scriptable Objects/SearchTask")]
public class SearchTask : Task
{
    [SerializeField] DataStorageTask DataToBeSearched;

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}
