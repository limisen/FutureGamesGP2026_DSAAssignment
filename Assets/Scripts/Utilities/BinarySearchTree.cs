using System;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Employee EmployeeData;
    public DateTime Value;
    public Node Left;
    public Node Right;
    public Node(Employee employeeData)
    {
        EmployeeData = employeeData;
        Value = employeeData.lastEntranceInEngineRoomDate;
        Left = null;
        Right = null;
    }
}

public class BinarySearchTree
{
    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    #region Insert Code
    public void Insert(Employee employee)
    {
        // Start the recursive insertion process from the root node.
        // A user can call this method to insert a new employee into the BST,
        // and it will handle the logic of finding the correct position for the new employee
        root = InsertRec(root, employee);
    }

    // Recursive function to insert a new value in the BST
    private Node InsertRec(Node currentNode, Employee employee)
    {
        // If the current node is null, create a new node with the value and return this Node.
        if (currentNode == null)
        {
            currentNode = new Node(employee);
            return currentNode;
        }

        // If the value is less than or equal to the current node's value, go left;
        if (employee.lastEntranceInEngineRoomDate <= currentNode.Value)
        {
            currentNode.Left = InsertRec(currentNode.Left, employee);
        }
        // otherwise, go right;
        else if (employee.lastEntranceInEngineRoomDate > currentNode.Value)
        {
            currentNode.Right = InsertRec(currentNode.Right, employee);
        }

        // This return makes sure that the parent node's left or right child is updated correctly after the recursive call.
        return currentNode;
    }
    #endregion End of Insert Code

    #region Singular Search Code
    public Node SingularSearchBST(DateTime value)
    {
        // Start the recursive search process from the root node.
        // A user can call this method to search for a value in the BST,
        // and it will handle the logic of traversing the tree to find the value.
        return SingularSearchBSTRec(root, value);
    }

    // Recursive function to search for a value in the BST
    private Node SingularSearchBSTRec(Node currentNode, DateTime value)
    {
        // If the current node is null, the value is not found.
        if (currentNode == null)
        {
            return null;
        }

        // If the value is equal to the current node's value, we have found the value we are searching for.
        if (value == currentNode.Value)
        {
            return currentNode;
        }

        // If the value is less than the current node's value, search in the left subtree.
        if (value < currentNode.Value)
        {
            return SingularSearchBSTRec(currentNode.Left, value);
        }

        // If the value is greater than the current node's value, search in the right subtree.
        return SingularSearchBSTRec(currentNode.Right, value);
    }
    #endregion End of Singular Search Code

    #region Range Search Code
    public List<Employee> RangeSearchBST(DateTime minValue, DateTime maxValue)
    {
        // Start the recursive range search process from the root node.
        // A user can call this method to search for values within a specified range in the BST,
        // and it will handle the logic of traversing the tree to find all values within that range.
        List<Employee> foundEmployees = RangeSearchBSTRec(root, minValue, maxValue, new List<Employee>());
        return foundEmployees;
    }

    // Recursive function to search for values within a specified range in the BST
    private List<Employee> RangeSearchBSTRec(Node currentNode, DateTime minValue, DateTime maxValue, List<Employee> foundEmployees)
    {
        // If the current node is null, return as there are no more nodes to check.
        if (currentNode == null)
        {
            return foundEmployees;
        }
        // If the current node's value is within the specified range, add the employee to the list.
        if (currentNode.Value >= minValue && currentNode.Value <= maxValue)
        {
            foundEmployees.Add(currentNode.EmployeeData);
        }
        // If the current node's value is greater than the minimum value, search in the left subtree.
        if (currentNode.Value > minValue)
        {
            RangeSearchBSTRec(currentNode.Left, minValue, maxValue, foundEmployees);
        }
        // If the current node's value is less than the maximum value, search in the right subtree.
        if (currentNode.Value < maxValue)
        {
            RangeSearchBSTRec(currentNode.Right, minValue, maxValue, foundEmployees);
        }
        return foundEmployees;
    }
    #endregion End of Range Search Code
}
