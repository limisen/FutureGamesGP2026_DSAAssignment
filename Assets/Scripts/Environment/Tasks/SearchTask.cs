using Environment.Tasks;
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchTask", menuName = "Scriptable Objects/SearchTask")]
public class SearchTask : Task
{
    [SerializeField] DataStorageTask DataToBeSearched;

    public override void Execute()
    {
        #region Binary Search
        DateTime searchArg = DateTime.Parse("2021-08-22T06:55:12");
        Employee killer = BinarySearch(DataToBeSearched.employees, searchArg);

        #endregion End of Binary Search code
    }

    // assuming the collection is sorted search it for the specified searchArg
    private Employee BinarySearch(Employee[] DataToBeSearched, DateTime searchArg)
    {
        // Early Exit condition so we dont recurse infinitely
        if (DataToBeSearched.Length < 1) { return null; }


        int median = DataToBeSearched.Length / 2;

        // If the element at the median index == searchArg, return that element
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate == searchArg) { return DataToBeSearched[median]; }

        // If this element is less than the median: decrease the size of the collection so that it contains the half with larger elements
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate < searchArg)
        {
            Employee[] partitionedCollection = new Employee[median];

            int j = 0;
            for (int i = median; i < DataToBeSearched.Length - 1; i++)
            {
                partitionedCollection[j] = DataToBeSearched[i];
                j++;
            }

            // Return the element if found otherwise null
            return BinarySearch(partitionedCollection, searchArg);
        }
        //  If this element is larger than the median: take the collection with smaller elements
        if (DataToBeSearched[median].lastEntranceInEngineRoomDate > searchArg)
        {
            Employee[] partitionedCollection = new Employee[median];
            for (int i = 0; i < median; i++)
            {
                partitionedCollection[i] = DataToBeSearched[i];
            }

            // Return the element if found otherwise null
            return BinarySearch(partitionedCollection, searchArg);
        }

        // otherwise return null, indicating that the argument we are searching for doesnt exist in the collection.
        return null;
    }
}
