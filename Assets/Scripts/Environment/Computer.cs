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
                currentTask = queue.Peek();
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
                currentTask = queue.Peek();

                currentTask.Execute();

                currentTask = queue.Dequeue();
            }
        }
    }
}
