using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Awaitables
{
    public class AwaitableQueue
    {
        private readonly ConcurrentQueue<Func<Awaitable>> queue = new();
        private bool isExecuting;

        public void Enqueue(Func<Awaitable> task)
        {
            queue.Enqueue(task);
            if (!isExecuting) _ = ExecuteAllTasks();
        } 
        
        public async Awaitable ExecuteAllTasks()
        {
            isExecuting = true;
            
            while (queue.TryDequeue(out var task))
            {
                await task();
            }
            
            isExecuting = false;
        }
        
        public void ClearTasks()
        {
            while (queue.TryDequeue(out _)) { }
        }
    }
}