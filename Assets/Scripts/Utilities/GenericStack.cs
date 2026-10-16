using System.Collections.Generic;
using UnityEngine;

public class GenericStack<T>
{
    private List<T> stack;

    private int count;

    public int Count { get { return count; } }

    public GenericStack()
    {
        stack = new List<T>();
        count = 0;
    }

    /// <summary> Adds the passed in element to the top of the Stack </summary>
    public void Push(T InNewElement)
    {
        count++;
        stack.Insert(count -1, InNewElement);
    }

    /// <summary> Removes the element at the top of the stack </summary>
    /// <returns> Returns the removed element </returns>
    public T Pop()
    {
        if (count == 0)
        {
            throw new System.Exception("Stack is empty, cannot Pop");
        }

        T ElementRemoved = stack[count -1];
        count--;
        stack.RemoveAt(count);

        return ElementRemoved;
    }

    /// <summary> Returns the element at the top of the stack </summary>
    public T Peek()
    {
        if (count == 0)
        {
            throw new System.Exception("Queue is empty, cannot Peek");
        }

        Debug.Log(count);
        return stack[count -1];
    }

    /// <summary> Removes all the elements in the stack </summary>
    public void Clear()
    {
        stack.Clear();
        count = 0;
    }
}
