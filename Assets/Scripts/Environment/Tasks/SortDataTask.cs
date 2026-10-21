using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SortDataTask", menuName = "Scriptable Objects/SortDataTask")]
public class SortDataTask : Task
{
    [SerializeField] DataStorageTask DataToBeSorted;


    public override void Execute()
    {
        int[] arrayToSort = {5, 2, 7, 3, 4, 8, -1 };
        
        int lastCheckedElement = 1;
        int i = lastCheckedElement;

        while (lastCheckedElement < arrayToSort.Length)
        {
            i = lastCheckedElement;
            while (i != 0 && arrayToSort[i] < arrayToSort[i - 1])
            {
                int temp = arrayToSort[i - 1];

                arrayToSort[i - 1] = arrayToSort[i];

                arrayToSort[i] = temp;

                i--;
            }
            lastCheckedElement++;
        }
    }
}
