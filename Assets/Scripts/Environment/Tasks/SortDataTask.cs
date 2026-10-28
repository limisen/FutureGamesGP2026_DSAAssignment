using Environment.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SortDataTask", menuName = "Scriptable Objects/SortDataTask")]
public class SortDataTask : Task
{
    [SerializeField] DataStorageTask DataToBeSorted;


    public override void Execute()
    {
        //int[] arrayToSort = { 8, 2, 7, 3, 4, -2, -1 };
        Employee[] arrayToSort = DataToBeSorted.employees;
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
        //arrayToSort = MergeSort(arrayToSort);
        arrayToSort = MergeSort(arrayToSort);
        #endregion End of Merge Sort Code

        #region Quick Sort
        //arrayToSort = QuickSort(arrayToSort);
        #endregion End of Quick Sort Code
    }

    #region Merge Sort
    private Employee[] MergeSort(Employee[] arrayToSort)
    {
        // Exit Early if the Collection is too small to split.
        if (arrayToSort.Length <= 1) { return arrayToSort; }

        // With how C# does Int division it results in the floor of the division result. aka 1 / 2 = 1
        int mid = arrayToSort.Length / 2;

        // Split the collection and recursivley call this function untill both the first- and second-Halves are of size 1
        Employee[] firstHalf = MergeSort(arrayToSort.Take(mid).ToArray());
        Employee[] secondHalf = MergeSort(arrayToSort.Skip(mid).ToArray());

        // Merge the two collections into the arrayToSort via a helper (Merge) method, Ultimatelly returning the now sorted collection
        return Merge(firstHalf, secondHalf);
    }

    private Employee[] Merge(Employee[] firstHalf, Employee[] secondHalf)
    {
        // define the size of the return collection
        Employee[] sortedArray = new Employee[firstHalf.Length + secondHalf.Length];

        // set-up our itteratiors
        int i = 0;
        int j = 0;
        int k = 0;

        // Compare the elements and assign them their correct position in the collection to be returned
        while (i < firstHalf.Length && j < secondHalf.Length)
        {
            if (firstHalf[i].lastEntranceInEngineRoomDate <= secondHalf[j].lastEntranceInEngineRoomDate)
            {
                sortedArray[k] = firstHalf[i];
                i++;
            }
            else
            {
                sortedArray[k] = secondHalf[j];
                j++;
            }
            // increase the itterator so we assign to the next spot in the return collection
            k++;
        }

        // incase we finish comparring elements with out checking them all. Add them to the end of the return collection, (prioratising the fistHalf).
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
    //private int[] QuickSort(int[] arrayToSort)
    //{
    //    if (arrayToSort.Length <= 1) { return arrayToSort; }

    //    // Pick a pivot (element)
    //    int pivot = Random.Range(0, arrayToSort.Length);
    //    int pivotElemnet = arrayToSort[pivot];

    //    int[] lowerValues = new int[pivot];
    //    int[] higherValues = new int[arrayToSort.Length - pivot];

    //    int i = 0;
    //    int j = 0;

    //    for (int k = 0; k < arrayToSort.Length; k++)
    //    {
    //        if (arrayToSort[k] <= pivotElemnet)
    //        {
    //            lowerValues[i] = arrayToSort[k];
    //            i++;
    //        }
    //        else
    //        {
    //            higherValues[j] = arrayToSort[k];
    //            j++;
    //        }
    //    }
    //    QuickSort(lowerValues);
    //    QuickSort(higherValues);

    //    // Return the sorted collection
    //    return arrayToSort;
    //}
    #endregion End of Qick Sort Code
}
