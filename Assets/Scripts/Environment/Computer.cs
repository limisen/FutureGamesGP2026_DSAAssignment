using Environment.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class Computer : MonoBehaviour, IInteractable
    {
        private GenericQueue<Task> queue = new GenericQueue<Task>();

        [SerializeField] private Task currentTask = null;

        [SerializeField] private List<Task> startUpTasks = new List<Task>();

        private void Awake()
        {
            Debug.Log(queue.Count);
            if (queue.Count != 0)
            {
                currentTask = queue.Peek();
            }
            else
            {
                foreach (var item in startUpTasks)
                {
                    queue.Enqueue(item);
                    //Debug.Log(queue.Count);
                }
            }
        }

        public void OnInteract()
        {
            if (queue.Count == 0)
            {
                Debug.Log($"Que is of size {queue.Count}, Get more tasks before trying to execute the next one.");
                currentTask = null;
                return;
            }
            else
            {
                currentTask = queue.Dequeue();

                currentTask.Execute();
            }
        }
    }
}

#region Using the stack data structure
/* 
    private Stack<Task> stack = new Stack<Task>();

        [SerializeField] private Task currentTask = null;

        [SerializeField] private List<Task> startUpTasks = new List<Task>();

        private void Awake()
        {
            Debug.Log(stack.Count);
            if (stack.Count != 0)
            {
                currentTask = stack.Peek();
            }
            else
            {
                foreach (var item in startUpTasks)
                {
                    stack.Push(item);
                    //Debug.Log(stack.Count);
                }
                currentTask = stack.Peek();
            }
        }

        public void OnInteract()
        {
            if (stack.Count == 0)
            {
                Debug.Log($"Stack is of size {stack.Count}, Get more tasks before trying to execute the next one.");
                currentTask = null;
                return;
            }
            else
            {
                currentTask = stack.Pop();

                currentTask.Execute();

                
            }
        }
*/
#endregion