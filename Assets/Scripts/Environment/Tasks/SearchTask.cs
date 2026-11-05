using Environment.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchTask", menuName = "Scriptable Objects/SearchTask")]
public class SearchTask : Task
{
    [SerializeField] DataStorageTask DataToBeSearched;

    [SerializeField] string SearchArgMinString;
    [SerializeField] string SearchArgMaxString;

    [SerializeField] string SingularSearchArgString;

    [SerializeField] Employee foundElement;
    [SerializeField] List<Employee> foundElements;
    [SerializeField] List<Employee> BSTfoundElements;


    public override void Execute()
    {
        #region Singular Binary Search
        DateTime SingularSearchArg = DateTime.Parse(SingularSearchArgString);

        Debug.Log($"Starting search for a element with the specified date: {SingularSearchArg}");
        System.Diagnostics.Stopwatch SingularSearchStopwatch = System.Diagnostics.Stopwatch.StartNew();

        Employee killer = SingularBinarySearch(DataToBeSearched.employeeArray, SingularSearchArg);

        SingularSearchStopwatch.Stop();

        long SingularSearchTimeToExecute = SingularSearchStopwatch.ElapsedTicks;

        //Debug.Log($"Singular Search took: {SingularSearchTimeToExecute} ticks to complete.");

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

        System.Diagnostics.Stopwatch RangedSearchStopwatch = System.Diagnostics.Stopwatch.StartNew();
        //Benchmark.StartBenchmark();

        List<Employee> killers = RangedBinarySearch(DataToBeSearched.employeeArray, SearchArgMin, SearchArgMax);

        //Benchmark.EndBenchmark();
        RangedSearchStopwatch.Stop();

        long RangedSearchTimeToExecute = RangedSearchStopwatch.ElapsedTicks;

        //Debug.Log($"Ranged Search took: {RangedSearchTimeToExecute} ticks to complete.");
        //Debug.Log($"Ranged Search took: {Benchmark.timeTaken} seconds to complete.");

        foundElements = killers; // Assign the entire list to foundElement since RangedBinarySearch returns a list of elements that match the search criteria.

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
        System.Diagnostics.Stopwatch BSTSearchStopwatch = System.Diagnostics.Stopwatch.StartNew(); // Start the stopwatch to measure the time taken for the search operation

        // Start the search for elements within the specified range in the Binary Search Tree
        BSTfoundElements = BST_Employees.RangeSearchBST(BSTSearchArgMin, BSTSearchArgMax);

        BSTSearchStopwatch.Stop(); // Stop the stopwatch after the search operation is complete

        long BSTSearchTimeToExecute = BSTSearchStopwatch.ElapsedTicks;

        //Debug.Log($"Binary Search Tree Search took: {BSTSearchTimeToExecute} ticks to complete.");

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
        Debug.Log($"Singular Binary Search: {SingularSearchTimeToExecute} ticks");
        Debug.Log($"Ranged Binary Search: {RangedSearchTimeToExecute} ticks");
        Debug.Log($"Binary Search Tree Search: {BSTSearchTimeToExecute} ticks");
    }

    #region Singular Binary Search
    // assuming the collection is sorted(in ascending order), search it for an element that matches the specified searchArg, return the element if found, otherwise return null.
    private Employee SingularBinarySearch(Employee[] DataToBeSearched, DateTime searchArg)
    {
        // Early Exit condition so we dont recurse infinitely
        if (DataToBeSearched.Length < 1) { return null; }

        // Calculate/find the median of the collection
        int median = DataToBeSearched.Length / 2;

        // If the element at the median index is equal to the searchArg, return that element
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate == searchArg) { return DataToBeSearched[median]; }

        // If this element is less than the median: create and take the collection that contains the half with larger elements
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArg)
        {
            Employee[] partitionedCollection = new Employee[median];
            int j = 0;
            for (int i = median; i < DataToBeSearched.Length - 1; i++)
            {
                partitionedCollection[j] = DataToBeSearched[i];
                j++;
            }
            // Return the element if found
            return SingularBinarySearch(partitionedCollection, searchArg);
        }

        //  If this element is larger than the median: create and take the collection that contains the collection with smaller elements
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate > searchArg)
        {
            Employee[] partitionedCollection = new Employee[median];
            for (int i = 0; i < median; i++)
            {
                partitionedCollection[i] = DataToBeSearched[i];
            }
            // Return the element if found
            return SingularBinarySearch(partitionedCollection, searchArg);
        }

        // otherwise return null, indicating that there are no elements in the specified range.
        return null;
    }
    #endregion End of code for Singular Binary Search

    #region Ranged Binary Search
    //assuming the collection is sorted(in ascending order), search it for elements within the specified range[searchArgMin, searchArgMax]
    private List<Employee> RangedBinarySearch(Employee[] DataToBeSearched, DateTime searchArgMin, DateTime searchArgMax)
    {
        // Early Exit condition so we dont recurse infinitely
        if (DataToBeSearched.Length <= 1)
        {
            // If the only element in the collection is within the range, return a collection with that element in it.
            if (DataToBeSearched[0].lastEntranceInEngineRoomDate >= searchArgMin && DataToBeSearched[0].lastEntranceInEngineRoomDate <= searchArgMax)
            {
                List<Employee> singleElementList = new List<Employee>();
                singleElementList.Add(DataToBeSearched[0]);
                return singleElementList;
            }
            // Otherwise return null, indicating that there are no elements in the specified range.
            return null;
        }

        // Calculate/find the median of the collection
        int median = DataToBeSearched.Length / 2;
        if (median % 2 > 0 && median + 1 < DataToBeSearched.Length) { median++; } // if the median is odd and increasing it is within bounds, increase it so we can split the collection in halves evenly

        // If this element (at the median index) is less than searchArgMin: create a collection that contains the half with larger elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArgMin)
        {
            Employee[] partitionedCollection = new Employee[median];

            int j = 0;
            for (int i = median; i < DataToBeSearched.Length; i++)
            {
                // if the element is null that indicates that we have reached the end of the collection
                if (DataToBeSearched[i] == null)
                {
                    // Create a new collection that is the size of the number of elements we have added to the partitionedCollection so far.
                    Employee[] newPartitionedCollection = new Employee[j];
                    for (int k = 0; k < j; k++)
                    {
                        // Copy these elements from the partitionedCollection into this new collection.
                        newPartitionedCollection[k] = partitionedCollection[k];
                    }
                    // Then return this new collection
                    return RangedBinarySearch(newPartitionedCollection, searchArgMin, searchArgMax);
                }
                partitionedCollection[j] = DataToBeSearched[i];
                j++;
            }

            // Return the element if found
            return RangedBinarySearch(partitionedCollection, searchArgMin, searchArgMax);
        }

        //  If this element (at the median index) is larger than searchArgMax: create a collection that contains the collection with smaller elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate > searchArgMax)
        {
            Employee[] partitionedCollection = new Employee[median];

            for (int i = 0; i < median; i++)
            {
                // if the element is null that indicates that we have reached the end of the collection
                if (DataToBeSearched[i] == null)
                {
                    // Create a new collection that is the size of the number of elements we have added to the partitionedCollection so far.
                    Employee[] newPartitionedCollection = new Employee[i];
                    for (int k = 0; k < i; k++)
                    {
                        // Copy these elements from the partitionedCollection into this new collection.
                        newPartitionedCollection[k] = partitionedCollection[k];
                    }
                    // Then return this new collection
                    return RangedBinarySearch(newPartitionedCollection, searchArgMin, searchArgMax);
                }

                partitionedCollection[i] = DataToBeSearched[i];
            }

            // Otherwise return the partitioned collection, meaning no elements are null.
            return RangedBinarySearch(partitionedCollection, searchArgMin, searchArgMax);
        }

        // If the element at the median index is within the range [searchArgMin, searchArgMax], we need to find all elements in this range:

        //Employee[] EmployeesMatchingSearchArg = DataToBeSearched.Where(employee => employee.lastEntranceInEngineRoomDate >= searchArgMin && employee.lastEntranceInEngineRoomDate <= searchArgMax).ToArray();
        List<Employee> EmployeesMatchingSearchArg = new List<Employee>();

        // If the element at the median index is within the range searchArgMin to searchArgMax,
        // add that element to the collection of elements we are returning.
        // Then check the elements before the median to see if they also match the search criteria. IF so add them to the return collection as well.
        // Then check the elements after the median to see if they also match the search criteria. IF so add them to the return collection as well.
        // IF the element before or after the median do not match the search criteria, return the collection of elements that match the search criteria.
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate >= searchArgMin
            && DataToBeSearched[median].lastEntranceInEngineRoomDate <= searchArgMax)
        {
            // check the elements before the median to see if these elements also match the search criteria. IF so add them to the return collection as well.
            int i = median - 1;
            while (i >= 0)
            {
                if (DataToBeSearched[i].lastEntranceInEngineRoomDate >= searchArgMin
                    && DataToBeSearched[i].lastEntranceInEngineRoomDate <= searchArgMax)
                {
                    EmployeesMatchingSearchArg.Add(DataToBeSearched[i]);
                }
                else
                {
                    break; // exit the loop if the element does not match the search criteria
                }
                // decrease the itterator so we check the previous element in the collection
                i--;
            }
            // Add the median element to the collection of elements since it also matches the search criteria.
            EmployeesMatchingSearchArg.Add(DataToBeSearched[median]);

            // check the elements after the median to see if these elements also match the search criteria. IF so add them to the return collection as well.
            int j = median + 1;
            while (j < DataToBeSearched.Length)
            {
                if (DataToBeSearched[j].lastEntranceInEngineRoomDate >= searchArgMin
                    && DataToBeSearched[j].lastEntranceInEngineRoomDate <= searchArgMax)
                {
                    EmployeesMatchingSearchArg.Add(DataToBeSearched[j]);
                }
                else
                {
                    break; // exit the loop if the element does not match the search criteria
                }
                // increase the itterator so we check the next element in the collection
                j++;
            }

            // Return the collection of elements that match the search criteria.
            return EmployeesMatchingSearchArg;
        }

        // otherwise return null, indicating that there are no elements in the specified range.
        return null;
    }
    #endregion End of code for Ranged Binary Search
}
