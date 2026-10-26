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
        //int maxItteration = 0;

        //bool hasSwapped = false;
        //// outer loop
        //while (((maxItteration * -1) + 1) != arrayToSort.Length)
        //{
        //    hasSwapped = false;
        //    // inner loop
        //    for (int i = 1; i < arrayToSort.Length + maxItteration; i++)
        //    {
        //        // move element towards the back
        //        if (arrayToSort[i] < arrayToSort[i - 1])
        //        {
        //            int temp = arrayToSort[i];

        //            arrayToSort[i] = arrayToSort[i - 1];

        //            arrayToSort[i - 1] = temp;

        //            hasSwapped = true;
        //        }

        //        // if at end of the collection decrease maxItteration so we dont go over the already sorted elements
        //        if (i + 1 == arrayToSort.Length + maxItteration)
        //        {
        //            maxItteration--;
        //        }
        //    }
        //    // IF we havn't swapped, assume the collection is sorted and break out of the loop
        //    if (!hasSwapped) { break; }
        //}
        #endregion End of Bubble Sort Code

        #region Merge Sort
        // Incomplete Implimentation 
        arrayToSort = MergeSort(arrayToSort);
        #endregion End of Merge Sort Code

        #region Quick Sort

        #endregion End of Quick Sort Code
    }

    #region Merge Sort
    // Incomplete Implimentation 
    private int[] MergeSort(int[] arrayToSort)
    {
        // Exit Early if the Collection is too small to split. Meaning it's already sorted by default.
        if (arrayToSort.Length <= 1) { return arrayToSort; }

        // With how C# does Int division it results in the floor of the division result. aka 1 / 2 = 1
        int mid = arrayToSort.Length / 2;

        // Split the collection and recursivley call this function untill both the first- and second-Halves are of size 1
        int[] firstHalf = MergeSort(arrayToSort.Take(mid).ToArray());
        int[] secondHalf = MergeSort(arrayToSort.Skip(mid).ToArray());

        // Merge the two collections into the arrayToSort via a helper method
        return Merge(firstHalf, secondHalf);
    }

    private int[] Merge(int[] firstHalf, int[] secondHalf)
    {
        // define the size of the return collection
        int[] sortedArray = new int[firstHalf.Length + secondHalf.Length];

        // set-up our itteratiors
        int i = 0;
        int j = 0;
        int k = 0;

        // Compare the elements and assign them their correct position in the collection to be returned
        while (i < firstHalf.Length && j < secondHalf.Length)
        {
            if (firstHalf[i] <= secondHalf[j])
            {
                sortedArray[k] = firstHalf[i];
                i++;
            }
            else
            {
                sortedArray[k] = secondHalf[j];
                j++;
            }
            // increase the itterator so we assign to the next spot in the collection
            k++;
        }

        // incase we are in a extreeme scenario make sure no elements get missed.
        while (i < firstHalf.Length)
        {
            sortedArray[k] = firstHalf[i];
            i++;
            k++;
        }

        while (j < secondHalf.Length)
        {
            sortedArray[k] = secondHalf[j];
            j++;
            k++;
        }

        return sortedArray;
    }

    #endregion End of Merge Sort Code

    #region Quick Sort

    #endregion End of Qick Sort Code
}
