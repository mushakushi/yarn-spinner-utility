using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace YarnSpinnerUtility.Runtime.Awaitables
{
    /// <summary>
    /// Represents a queue that handles tasks which can be awaited. Tasks are executed sequentially, with support for cancellation.
    /// </summary>
    public class AwaitableQueue
    {
        /// <summary>
        /// A concurrent queue to store tasks. Each task is a function that returns an <see cref="Awaitable"/>.
        /// </summary>
        private readonly ConcurrentQueue<Func<CancellationToken, Awaitable>> queue = new();
    
        /// <summary>
        /// Indicates whether the queue is currently executing tasks.
        /// </summary>
        private bool isExecuting;

        /// <summary>
        /// Adds a task to the queue and starts execution if the queue is not currently executing.
        /// </summary>
        /// <param name="task">The task to enqueue. It is a function that accepts a <see cref="CancellationToken"/> and returns an <see cref="Awaitable"/>.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        public void Enqueue(Func<CancellationToken, Awaitable> task, CancellationToken cancellationToken = default)
        {
            queue.Enqueue(task);
            if (!isExecuting) ExecuteAllTasks(cancellationToken);
        }

        /// <summary>
        /// Executes all tasks in the queue sequentially. The execution will continue until the queue is empty or a cancellation is requested.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>An <see cref="Awaitable"/> that completes when all tasks in the queue have been executed, or when cancellation is requested.</returns>
        public async void ExecuteAllTasks(CancellationToken cancellationToken)
        {
            isExecuting = true;

            try
            {
                while (queue.TryDequeue(out var task))
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    await task(cancellationToken);
                }
            }
            finally
            {
                isExecuting = false;
            }
        }

        /// <summary>
        /// Clears all tasks from the queue without executing them.
        /// </summary>
        public void ClearTasks()
        {
            while (queue.TryDequeue(out _)) { }
        }
    }
}
