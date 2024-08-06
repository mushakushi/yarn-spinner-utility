using System.Collections.Generic;
using UnityEngine;

namespace Mushakushi.InkDialogueWriter.Runtime.Events
{
    public class DialogueEventHandler: MonoBehaviour
    {
        [field: SerializeField]
        protected DialogueEventChannel DialogueEventChannel { get; set; }

        private HashSet<DialogueIOBinding> bindings;

        protected void OnEnable()
        {
            DialogueEventChannel.OnBindStarted += HandleOnBindStarted;
            DialogueEventChannel.OnRead += HandleOnRead;
            DialogueEventChannel.OnReadToEnd += HandleOnReadToEnd;
            DialogueEventChannel.OnUnbindStarted += HandleOnUnbindStarted;
        }
        
        protected void OnDisable()
        {
            DialogueEventChannel.OnBindStarted -= HandleOnBindStarted;
            DialogueEventChannel.OnRead -= HandleOnRead;
            DialogueEventChannel.OnReadToEnd -= HandleOnReadToEnd;
            DialogueEventChannel.OnUnbindStarted -= HandleOnUnbindStarted;
        }

        protected void Awake()
        {
            bindings = new HashSet<DialogueIOBinding>();
        }

        private void HandleOnBindStarted(DialogueIOBinding binding)
        {
            bindings.Add(binding);
            binding.output.Open();
        }
        
        private void HandleOnRead(DialogueIOBinding binding)
        {
            if (!bindings.TryGetValue(binding, out var actualBinding)) return;
            if (IsEndOfStreamOrOutput(actualBinding)) return;
            actualBinding.output.Write(new string(actualBinding.stream.Read(), 1));
        }

        private void HandleOnReadToEnd(DialogueIOBinding binding)
        {
            if (!bindings.TryGetValue(binding, out var actualBinding)) return;
            if (IsEndOfStreamOrOutput(actualBinding)) return;
            actualBinding.output.Write(actualBinding.stream.ReadToEnd());
        }
        
        private void HandleOnUnbindStarted(DialogueIOBinding binding)
        {
            if (!bindings.TryGetValue(binding, out var actualBinding)) return;
            bindings.Remove(actualBinding);
            actualBinding.output.Close();
        } 

        private static bool IsEndOfStreamOrOutput(DialogueIOBinding binding)
        {
            return binding.stream.IsEndOfStream || binding.output.IsEndOfOutput; 
        }
    }
}