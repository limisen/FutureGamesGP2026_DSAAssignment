using Environment.Tasks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SecretTask", menuName = "Scriptable Objects/SecretTask")]
public class SecretTask : Task
{
    [SerializeField] List<int> secretValues = new List<int>() { 8, 2, 1, 3, 9 };

    public override void Execute()
    {
        int i = 0;

        Debug.Log("Displaying starting content: ");
        DisplayContent(secretValues);

        // Outer Loop
        for (int j = 0; j < secretValues.Count; j++)
        {
            // reset index
            i = 0;
            // inner loop
            while (i != secretValues.Count - 1)
            {
                Compare(secretValues, i);
                i++;
            }
        }
    }

    private void Compare(List<int> inputList, int index)
    {
        // swap the two elements, IF the element at index + 1 is smaller than the element at index. 
         if (inputList[index] > inputList[index + 1])
        {
            Debug.Log("Content has been changed...");
            int temp = inputList[index];

            Debug.Log($"Swapping '{inputList[index]}' with '{inputList[index + 1]}'");

            inputList[index] = inputList[index + 1];
            inputList[index + 1] = temp;

            DisplayContent(inputList);
        }
    }

    // Display the entierty of the list on one line seperating each element by " ".
    private void DisplayContent(List<int> inputList)
    {
        string inputListContent = " ";
        for (int i = 0; i < inputList.Count; i++)
        {
            inputListContent += (inputList[i].ToString() + " ");
        }
        Debug.Log($"SecretValues: {inputListContent}");
    }
}
