using Environment.Tasks;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SortDataTask", menuName = "Scriptable Objects/SortDataTask")]
public class SortDataTask : Task
{
    [SerializeField] DataStorageTask DataToBeSorted;


    public override void Execute()
    {
        int[] arrayToSort = { 8, 2, 7, 3, 4, -2, -1 };
        #region Selection Sort
        //int lastCheckedElement = 1;
        //int i = lastCheckedElement;

        // while (lastCheckedElement < arrayToSort.Length)
        //{
        //    i = lastCheckedElement;
        //    while (i != 0 && arrayToSort[i] < arrayToSort[i - 1])
        //    {
        //        int temp = arrayToSort[i - 1];

        //        arrayToSort[i - 1] = arrayToSort[i];

        //        arrayToSort[i] = temp;

        //        i--;
        //    }
        //    lastCheckedElement++;
        //}
        #endregion End of Selection Sort Code

        #region Bubble Sort
        int maxItteration = 0;

        bool hasSwapped = false;
        // outer loop
        while (((maxItteration * -1) + 1) != arrayToSort.Length)
        {
            hasSwapped = false;
            // inner loop
            for (int i = 1; i < arrayToSort.Length + maxItteration; i++)
            {
                // move element towards the back
                if (arrayToSort[i] < arrayToSort[i - 1])
                {
                    int temp = arrayToSort[i];

                    arrayToSort[i] = arrayToSort[i - 1];

                    arrayToSort[i - 1] = temp;

                    hasSwapped = true;
                }

                // if at end of the collection decrease maxItteration so we dont go over the already sorted elements
                if (i + 1 == arrayToSort.Length + maxItteration)
                {
                    maxItteration--;
                }
            }
            // IF we havnt swapped break out of the loop
            if (!hasSwapped) { break; }
        }
        #endregion End of Bubble Sort Code

        #region Merge Sort
        //Debug.Log("Unsorted: ");
        //foreach (var item in arrayToSort)
        //{
        //    Debug.Log(item);
        //}

        //arrayToSort = MergeSort(arrayToSort);

        //Debug.Log("Sorted: ");
        //foreach (var item in arrayToSort)
        //{
        //    Debug.Log(item);
        //}
        #endregion End of Merge Sort Code
    }

    // Incomplete Implimentation 
    //private int[] MergeSort(int[] arrayToSort, int[] firstHalf = new int[1], )
    //{
    //    int[] firstHalf;
    //    int[] secondHalf;

    //    int[] sortedArray;

    //    // Split the array
    //    if (arrayToSort.Length > 1)
    //    {
    //        int mid = Mathf.RoundToInt(arrayToSort.Length / 2);

    //        firstHalf = MergeSort(arrayToSort.Take(mid).ToArray());

    //        secondHalf = MergeSort(arrayToSort.Skip(mid).ToArray());
    //    }

    //    sortedArray = new int[firstHalf.Length + secondHalf.Length];

    //    return sortedArray;
    //}
}
