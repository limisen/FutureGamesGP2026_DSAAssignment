using Environment.Tasks;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SortDataTask", menuName = "Scriptable Objects/SortDataTask")]
public class SortDataTask : Task
{
    [SerializeField] DataStorageTask DataToBeSorted;

    private BinarySearchTree BST_Employees;

    public override void Execute()
    {
        //int[] arrayToSort = { 8, 2, 7, 3, 4, -2, -1 };
        Employee[] arrayToSort = DataToBeSorted.employeeArray;

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
        arrayToSort = MergeSort(arrayToSort);
        #endregion End of Merge Sort Code

        #region Quick Sort
        //arrayToSort = QuickSort(arrayToSort);
        #endregion End of Quick Sort Code

        DataToBeSorted.employeeArray = arrayToSort;

        #region Binary Search Tree
        // Create a new BST instance
        BST_Employees = new BinarySearchTree();

        // Call the BST insertion function
        BST_TopDownInsertion(arrayToSort, 0, arrayToSort.Length - 1);

        // Save it to DataStorageTask
        DataToBeSorted.BST_Employees = BST_Employees;
        #endregion End of Binary Search Tree Code
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
                sortedArray[k] = firstHalf[i]; // assign the element to the collection with smaller elements
                i++;
            }
            else
            {
                sortedArray[k] = secondHalf[j]; // assign the element to the collection with larger elements
                j++;
            }
            // increase the itterator so we assign to the next spot in the return collection
            k++;
        }

        // incase we finish comparring elements with out checking them all. Add the remaining elements to the end of the collection we are returning, (prioratising the fistHalf).
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

        // Return the now sorted collection
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

    #region Binary Search Tree insertion
    private void BST_TopDownInsertion(Employee[] SortedCollection, int minimumIndex, int maximumIndex)
    {
        // Early Exit condition so we dont recurse infinitely
        if (minimumIndex > maximumIndex)
        { return; }

        // Calculate/find the median of the collection
        int median = (minimumIndex + maximumIndex) / 2;

        // add the median to the BST
        BST_Employees.Insert(SortedCollection[median]);

        // Move left by half of (maximumIndex - minimumIndex)
        BST_TopDownInsertion(SortedCollection, minimumIndex, median - 1);

        // Move right by half of (maximumIndex - minimumIndex)
        BST_TopDownInsertion(SortedCollection, median + 1, maximumIndex);
    }
    #endregion End of Binary Search Tree insertion code
}
