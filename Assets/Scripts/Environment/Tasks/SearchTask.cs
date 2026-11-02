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

    public override void Execute()
    {
        #region Singular Binary Search
        DateTime SingularSearchArg = DateTime.Parse(SingularSearchArgString);

        Debug.Log($"Starting search for a element with specified date: {SingularSearchArg}");
        float singularSearchStartTime = Time.realtimeSinceStartup;

        Employee killer = SingularBinarySearch(DataToBeSearched.employees, SingularSearchArg);

        float singularSearchEndTime = Time.realtimeSinceStartup;

        Debug.Log($"Singular Search took: {singularSearchEndTime - singularSearchStartTime} seconds to complete.");

        if (foundElement != null)
        {
            Debug.Log($"Found a element that matches the search criteria: {foundElement}");
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

        float rangedSearchStartTime = Time.realtimeSinceStartup;

        List<Employee> killers = RangedBinarySearch(DataToBeSearched.employees, SearchArgMin, SearchArgMax);

        float rangedSearchEndTime = Time.realtimeSinceStartup;

        Debug.Log($"Ranged Search took: {rangedSearchEndTime - rangedSearchStartTime} seconds to complete.");

        foundElements = killers; // Assign the entire list to foundElement since RangedBinarySearch returns a list of elements that match the search criteria.

        if (foundElement != null)
        {
            Debug.Log("Found " + foundElements.Count + " elements that match the search criteria.");
        }
        else
        {
            Debug.Log("Found no elements that match the search criteria.");
        }

        #endregion End of Ranged Binary Search code

        Debug.Log($"Time difference between the two searches: {Math.Abs((singularSearchEndTime - singularSearchStartTime) - (rangedSearchEndTime - rangedSearchStartTime))} seconds.");
    }

    #region Singular Binary Search
    // assuming the collection is sorted search it for an element that matches the specified searchArg
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
    // assuming the collection is sorted search it for elements within the specified range [searchArgMin, searchArgMax]
    private List<Employee> RangedBinarySearch(Employee[] DataToBeSearched, DateTime searchArgMin, DateTime searchArgMax)
    {
        // Early Exit condition so we dont recurse infinitely
        if (DataToBeSearched.Length < 1) { return null; }

        // Calculate/find the median of the collection
        int median = DataToBeSearched.Length / 2;

        // If this element (at the median index) is less than searchArgMin: create a collection that contains the half with larger elements and then recurse on that collection
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArgMin)
        {
            Employee[] partitionedCollection = new Employee[median];

            int j = 0;
            for (int i = median; i < DataToBeSearched.Length - 1; i++)
            {
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
                partitionedCollection[i] = DataToBeSearched[i];
            }

            // Return the element if found
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
            // Add the median element to the collection of elements since it matches the search criteria.
            EmployeesMatchingSearchArg.Add(DataToBeSearched[median]);

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
            // check the elements after the median to see if these elements also match the search criteria. IF so add them to the return collection as well.
            int j = median + 1;
            while (j < DataToBeSearched.Length - 1)
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
