using System;
using Mushakushi.InkDialogueWriter.Runtime.Input;
using Mushakushi.InkDialogueWriter.Runtime.Output;
using UnityEngine;

namespace Mushakushi.InkDialogueWriter.Runtime.Events
{
    /// <summary>
    /// Callbacks for dialogue. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DialogueEventChannel), menuName = "ScriptableObjects/Ink Dialogue Writer/Dialogue Event Channel")]
    public class DialogueEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback on binding a <see cref="IDialogueStream"/> to a <see cref="IDialogueOutput"/>
        /// </summary>
        public event Action<DialogueIOBinding> OnBindStarted;
        
        /// <summary>
        /// Callback on <see cref="OnBindStarted"/> complete.
        /// </summary>
        public event Action<IDialogueOutput> OnBindCompleted;

        /// <summary>
        /// Outputs the next character in stream.
        /// </summary>
        public event Action<DialogueIOBinding> OnRead;

        /// <summary>
        /// Reads the remaining characters in the stream as a string.
        /// May overflow the output.
        /// </summary>
        public event Action<DialogueIOBinding> OnReadToEnd;

        /// <summary>
        /// Callback on unbinding a <see cref="IDialogueStream"/> from a <see cref="IDialogueOutput"/>.
        /// </summary>
        public event Action<DialogueIOBinding> OnUnbindStarted;

        /// <summary>
        /// Callback on <see cref="OnUnbindStarted"/>. 
        /// </summary>
        public event Action<IDialogueOutput> OnUnbindCompleted;

        /// <summary>
        /// Invokes <see cref="OnBindStarted"/>
        /// </summary>
        /// <param name="binding">The binding to bind.</param>
        public void InvokeOnBindStarted(DialogueIOBinding binding) => OnBindStarted?.Invoke(binding);
        
        /// <summary>
        /// Invokes <see cref="OnBindCompleted"/>
        /// </summary>
        /// <param name="binding">The output that was bound.</param>
        public void InvokeOnBindCompleted(IDialogueOutput binding) => OnBindCompleted?.Invoke(binding);

        /// <summary>
        /// Invokes <see cref="OnRead"/>
        /// </summary>
        /// <param name="binding">The binding that will perform the reading.</param>
        public void InvokeOnRead(DialogueIOBinding binding) => OnRead?.Invoke(binding);
        
        /// <summary>
        /// Invokes <see cref="OnReadToEnd"/>
        /// </summary>
        /// <param name="binding">The binding that will perform the reading.</param>
        public void InvokeOnReadToEnd(DialogueIOBinding binding) => OnReadToEnd?.Invoke(binding);
        
        /// <summary>
        /// Invokes <see cref="OnUnbindStarted"/>
        /// </summary>
        /// <param name="binding">The binding to unbind.</param>
        public void InvokeOnUnbindStarted(DialogueIOBinding binding) => OnUnbindStarted?.Invoke(binding);
        
        /// <summary>
        /// Invokes <see cref="OnUnbindCompleted"/>
        /// </summary>
        /// <param name="binding">The output that was unbound.</param>
        public void InvokeOnUnbindCompleted(IDialogueOutput binding) => OnUnbindCompleted?.Invoke(binding);
    }
}