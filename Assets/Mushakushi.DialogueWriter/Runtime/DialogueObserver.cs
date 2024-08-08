using System;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime
{
    /// <summary>
    /// Callbacks for dialogue. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DialogueObserver), menuName = "ScriptableObjects/Dialogue Writer/Dialogue Observer")]
    public class DialogueObserver : ScriptableObject
    {
        /// <summary>
        /// Callback on binding a <see cref="IInputStream"/> to a <see cref="IOutputStream"/>
        /// </summary>
        public RaisableEvent<DialogueIOBinding> onBindStarted;
        
        /// <summary>
        /// Callback on <see cref="onBindStarted"/> complete.
        /// </summary>
        public RaisableEvent<DialogueIOBinding> onBindCompleted;

        /// <summary>
        /// Outputs the next character in stream.
        /// </summary>
        public RaisableEvent<DialogueIOBinding> onRead;

        /// <summary>
        /// Callback on unbinding a <see cref="IInputStream"/> from a <see cref="IOutputStream"/>.
        /// </summary>
        public RaisableEvent<DialogueIOBinding> onUnbindStarted;

        /// <summary>
        /// Callback on <see cref="onUnbindStarted"/>. 
        /// </summary>
        public RaisableEvent<DialogueIOBinding> onUnbindCompleted;
    }
}