using System.Collections.Generic;

public class GenericQueue<T> {
    private List<T> queue;

    private int count;

    public int Count { get { return count; } }

    public GenericQueue()
    {
        queue = new List<T>();
        count = 0;
    }

    /// <summary> Adds the passed in element to the back of the queue </summary>
    public void Enqueue(T InNewElement)
    {
        queue.Add(InNewElement);
        count++;
    }

    /// <summary> Removes the element at the front of the queue </summary>
    /// <returns> Returns the removed element </returns>
    public T Dequeue() 
    {
        if(queue.Count == 0)
        {
            throw new System.Exception("Queue is empty, cannot dequeue");
        }

        T ElementRemoved = queue[0];
        queue.RemoveAt(0);
        count--;

        return ElementRemoved;
    }

    /// <summary> Returns the element at the front of the queue </summary>
    public T Peek()
    {
        if (queue.Count == 0)
        {
            throw new System.Exception("Queue is empty, cannot Peek");
        }

        return queue[0];
    }

    /// <summary> Removes all the elements in the queue </summary>
    public void Clear()
    {
        queue.Clear();
        count = 0;
    }
}
