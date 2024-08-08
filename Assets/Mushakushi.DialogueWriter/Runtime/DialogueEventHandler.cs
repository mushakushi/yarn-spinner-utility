using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime
{
    /// <summary>
    /// Handles <see cref="DialogueObserver"/> callbacks.
    /// </summary>
    public class DialogueEventHandler: MonoBehaviour
    {
        [SerializeField] protected DialogueObserver dialogueObserver; 

        private HashSet<DialogueIOBinding> bindings;

        protected void OnEnable()
        {
            dialogueObserver.onBindStarted.Event += HandleOnBindStarted;
            dialogueObserver.onRead.Event += HandleOnRead;
            dialogueObserver.onUnbindStarted.Event += HandleOnUnbindStarted;
        }
        
        protected void OnDisable()
        {
            dialogueObserver.onBindStarted.Event -= HandleOnBindStarted;
            dialogueObserver.onRead.Event -= HandleOnRead;
            dialogueObserver.onUnbindStarted.Event -= HandleOnUnbindStarted;
        }

        protected void Awake()
        {
            bindings = new HashSet<DialogueIOBinding>();
        }

        private void HandleOnBindStarted(DialogueIOBinding binding)
        { 
            UniTask.Create(async () =>
            {
                bindings.Add(binding);
                await UniTask.WhenAll(binding.inputStream.Open(), binding.outputStream.Open());
                dialogueObserver.onBindCompleted.RaiseEvent(binding);
            });
        }
        
        private void HandleOnRead(DialogueIOBinding binding)
        {
            if (!bindings.TryGetValue(binding, out var actualBinding)) return;
            if (IsEndOfStreamOrOutput(actualBinding)) return;
            actualBinding.outputStream.Write(actualBinding.inputStream.Read());
        }
        
        private void HandleOnUnbindStarted(DialogueIOBinding binding)
        {
            UniTask.Create(async () =>
            {
                if (!bindings.TryGetValue(binding, out var actualBinding)) return;
                bindings.Remove(actualBinding);
                await UniTask.WhenAll(binding.inputStream.Close(), binding.outputStream.Close());
                dialogueObserver.onUnbindCompleted.RaiseEvent(binding);
            });
        } 

        private static bool IsEndOfStreamOrOutput(DialogueIOBinding binding)
        {
            return binding.inputStream.IsEndOfStream || binding.outputStream.IsEndOfStream; 
        }
    }
}