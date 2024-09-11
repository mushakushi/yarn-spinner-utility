using System;
using System.Threading;
using UnityEngine;

namespace YarnSpinnerUtility.Runtime.Output
{
    public abstract class ViewController<T>: MonoBehaviour
    {
        [SerializeField] protected DialogueParser dialogueParser;
        [SerializeReference, SubclassSelector] private IView<T>[] views;
        private CancellationTokenSource cancellationTokenSource = new();

        /// <summary>
        /// Whether the view is updating. 
        /// </summary>
        private bool isViewUpdating;
        
        public event Action OnViewUpdated;
        public event Action OnViewCleared;

        protected virtual void OnDisable()
        {
            cancellationTokenSource.CancelAndDispose();
        }

        protected void InitializeAllViews()
        {
            foreach (var view in views) view.Initialize();
        }
        
        protected async Awaitable UpdateAllViews(T args)
        {
            cancellationTokenSource = new CancellationTokenSource();
            isViewUpdating = true;
            
            try
            {
                foreach (var view in views) await view.Update(args, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException) { }
            
            cancellationTokenSource.Dispose();
            isViewUpdating = false;
            OnViewUpdated?.Invoke();
        }
        
        protected void ClearAllViews()
        {
            foreach (var view in views) view.Clear();
            OnViewCleared?.Invoke();
        }

        public bool TryCancelViewUpdate()
        {
            if (!isViewUpdating) return false;
            cancellationTokenSource.CancelAndDispose();
            return true;
        } }
}