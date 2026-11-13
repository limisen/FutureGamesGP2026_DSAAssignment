using Environment.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchTask", menuName = "Scriptable Objects/SearchTask")]
public class SearchTask : Task
{
    [SerializeField] DataStorageTask DataToBeSearched;
    [Space]
    [SerializeField] string SearchArgMinString;
    [SerializeField] string SearchArgMaxString;
    [Space]
    [SerializeField] string SingularSearchArgString;
    [Space]
    [SerializeField] Employee foundElement;
    [SerializeField] List<Employee> foundElements;
    [SerializeField] List<Employee> BSTfoundElements;


    public override void Execute()
    {
        #region Singular Binary Search
        DateTime SingularSearchArg = DateTime.Parse(SingularSearchArgString);

        Debug.Log($"Starting search for a element with the specified date: {SingularSearchArg}");
        Benchmark.StartBenchmark();

        foundElement = SingularBinarySearch(DataToBeSearched.employeeArray, 0, DataToBeSearched.employeeArray.Length - 1, SingularSearchArg);

        Benchmark.EndBenchmark();

        float SingularSearchTimeToExecuteTicks = Benchmark.timeTakenTicks;
        float SingularSearchTimeToExecuteMilliseconds = Benchmark.timeTaken;

        if (foundElement != null)
        {
            Debug.Log($"Found a element that matches the search criteria: {foundElement.name}");
        }
        else
        {
            Debug.Log("Found no element that matches the search criteria.");
        }
        #endregion End of Singular Binary Search code

        #region Ranged Binary Search
        DateTime SearchArgMin = DateTime.Parse(SearchArgMinString);
        DateTime SearchArgMax = DateTime.Parse(SearchArgMaxString);

        // Just incase the user putes the range the wrong way around
        if (SearchArgMin > SearchArgMax)
        {
            DateTime temp = SearchArgMax;

            SearchArgMax = SearchArgMin;
            SearchArgMin = temp;
        }

        Debug.Log("Starting search for elements between: " + SearchArgMin + " and " + SearchArgMax);

        Benchmark.StartBenchmark();

        foundElements = RangedBinarySearch(DataToBeSearched.employeeArray, 0, DataToBeSearched.employeeArray.Length - 1, SearchArgMin, SearchArgMax);

        Benchmark.EndBenchmark();

        float RangedSearchTimeToExecuteTicks = Benchmark.timeTakenTicks;
        float RangedSearchTimeToExecuteMilliseconds = Benchmark.timeTaken;

        if (foundElements != null)
        {
            Debug.Log("Found " + foundElements.Count + " elements that match the search criteria.");
        }
        else
        {
            Debug.Log("Found no elements that match the search criteria.");
        }
        #endregion End of Ranged Binary Search code

        #region Binary (Range) Search Tree Search
        BinarySearchTree BST_Employees = DataToBeSearched.BST_Employees;

        DateTime BSTSearchArgMin = DateTime.Parse(SearchArgMinString);
        DateTime BSTSearchArgMax = DateTime.Parse(SearchArgMaxString);

        // Just incase the user putes the range the wrong way around
        if (BSTSearchArgMin > BSTSearchArgMax)
        {
            DateTime temp = BSTSearchArgMax;
            BSTSearchArgMax = BSTSearchArgMin;
            BSTSearchArgMin = temp;
        }

        Debug.Log("Starting search for elements between: " + BSTSearchArgMin + " and " + BSTSearchArgMax);
        
        Benchmark.StartBenchmark();

        // Start the search for elements within the specified range in the Binary Search Tree
        BSTfoundElements = BST_Employees.RangeSearchBST(BSTSearchArgMin, BSTSearchArgMax);

        Benchmark.EndBenchmark();

        float BSTSearchTimeToExecuteTicks = Benchmark.timeTakenTicks;
        float BSTSearchTimeToExecuteMilliseconds = Benchmark.timeTaken;

        if (BSTfoundElements != null && BSTfoundElements.Count > 0)
        {
            Debug.Log("BST Found " + BSTfoundElements.Count + " elements that match the search criteria.");
        }
        else
        {
            Debug.Log("BST Found no elements that match the search criteria.");
        }
        #endregion End of Binary (Range) Search Tree Search code

        Debug.Log($"Each search function took the following time to complete:");
        Debug.Log($"Singular Binary Search: {SingularSearchTimeToExecuteTicks} ticks");
        Debug.Log($"Ranged Binary Search: {RangedSearchTimeToExecuteTicks} ticks");
        Debug.Log($"Binary Search Tree Search: {BSTSearchTimeToExecuteTicks} ticks");

        Debug.Log($"Singular Binary Search: {SingularSearchTimeToExecuteMilliseconds} ms");
        Debug.Log($"Ranged Binary Search: {RangedSearchTimeToExecuteMilliseconds} ms");
        Debug.Log($"Binary Search Tree Search: {BSTSearchTimeToExecuteMilliseconds} ms");
    }

    #region Singular Binary Search
    // assuming the collection is sorted(in ascending order), search it for an element that matches the specified searchArg, return the element if found, otherwise return null.
    private Employee SingularBinarySearch(Employee[] DataToBeSearched, int minimumIndex, int maximumIndex, DateTime searchArg)
    {
        // Early Exit condition so we dont recurse infinitely
        if (maximumIndex - minimumIndex <= 1) { return null; }

        // Calculate/find the median of the collection
        int median = minimumIndex + ((maximumIndex - minimumIndex) / 2);

        // If the element at the median index is equal to the searchArg, return that element
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate == searchArg) { return DataToBeSearched[median]; }

        // If this element (at the median index) is less than searchArg: move/overwrite the mimimumIndex to be equal to median.
        // Effectivly creating a collection containing only larger elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArg)
        {
            minimumIndex = median + 1;

            return SingularBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArg);
        }

        // If this element (at the median index) is greater than searchArg: move/overwrite the maximumIndex to be equal to median.
        // Effectivly creating a collection containing only smaller elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate > searchArg)
        {
            maximumIndex = median - 1;

            return SingularBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArg);
        }

        // otherwise return null, indicating that there are no elements in the specified (index) range.
        return null;
    }
    #endregion End of code for Singular Binary Search

    #region Ranged Binary Search
    //assuming the collection is sorted(in ascending order), search it for elements within the specified indexes (minimumIndex, maximumIndex) and date range[searchArgMin, searchArgMax]
    private List<Employee> RangedBinarySearch(Employee[] DataToBeSearched, int minimumIndex, int maximumIndex, DateTime searchArgMin, DateTime searchArgMax)
    {
        // Early Exit condition so we dont recurse infinitely
        if (maximumIndex - minimumIndex <= 0)
        {
            int index = minimumIndex + ((maximumIndex - minimumIndex) / 2);

            // If the only element in the collection is within the date range, return (a collection with) that element.
            if (DataToBeSearched[index].lastEntranceInEngineRoomDate >= searchArgMin && DataToBeSearched[index].lastEntranceInEngineRoomDate <= searchArgMax)
            {
                List<Employee> singleElementList = new List<Employee>();
                singleElementList.Add(DataToBeSearched[index]);
                return singleElementList;
            }
            // Otherwise return null, indicating that there are no elements in the specified ranges.
            return null;
        }

        // Calculate/find the median of the collection
        int median = minimumIndex + ((maximumIndex - minimumIndex) / 2);


        // If this element (at the median index) is less than searchArgMin: move/overwrite the mimimumIndex to be equal to the median + 1.
        // Effectivly creating a collection containing only larger elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArgMin)
        {
            minimumIndex = median + 1;

            // Return the element if found
            return RangedBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArgMin, searchArgMax);
        }

        // If this element (at the median index) is greater than searchArgMax: move/overwrite the maximumIndex to be equal to the median - 1.
        // Effectivly creating a collection containing only smaller elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate > searchArgMax)
        {
            maximumIndex = median - 1;

            // Return the element if found
            return RangedBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArgMin, searchArgMax);
        }

        // If the element at the minimumIndex is equal or greater than searchArgMin, AND the element at the maximumIndex is equal or less than searchArgMax, create and fill the return collection with these elements
        if (DataToBeSearched[minimumIndex].lastEntranceInEngineRoomDate >= searchArgMin && DataToBeSearched[median].lastEntranceInEngineRoomDate <= searchArgMax)
        {
            List<Employee> EmployeesMatchingSearchArg = new List<Employee>();

            for (int i = minimumIndex; i < maximumIndex; i++)
            {
                EmployeesMatchingSearchArg.Add(DataToBeSearched[i]);
            }

            return EmployeesMatchingSearchArg;
        }
        // decrese the search span even further
        // raising the minimumIndex
        else if (DataToBeSearched[minimumIndex].lastEntranceInEngineRoomDate >= searchArgMin)
        { 
            minimumIndex++;
            return RangedBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArgMin, searchArgMax);
        }
        // Otherwise lowering the maximumIndex
        else if (DataToBeSearched[median].lastEntranceInEngineRoomDate <= searchArgMax)
        { 
            maximumIndex--;
            return RangedBinarySearch(DataToBeSearched, minimumIndex, maximumIndex, searchArgMin, searchArgMax);
        }

        // otherwise return null, indicating that there are no elements in the specified range.
        return null;
    }
    #endregion End of code for Ranged Binary Search
}
