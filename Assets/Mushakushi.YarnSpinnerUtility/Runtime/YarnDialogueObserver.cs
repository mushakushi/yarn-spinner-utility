using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    [CreateAssetMenu(fileName = nameof(YarnDialogueObserver), menuName = "ScriptableObjects/Yarn/Yarn Parser Observer")]
    public class YarnDialogueObserver : ScriptableObject
    {
        /// <summary>
        /// Callback on starting dialogue 
        /// </summary>
        /// <seealso cref="onNodeStarted"/>
        public RaisableEvent onStartDialogue; 
        
        /// <summary>
        /// Callback on continuing dialogue. 
        /// </summary>
        public RaisableEvent onContinueDialogue; 
        
        /// <summary>
        /// Callback on outputting a <see cref="LocalizedLine"/>.
        /// </summary>
        public RaisableEvent<LocalizedLine> onOutputLineRequested;
        
        /// <summary>
        /// Callback on <see cref="onOutputLineRequested"/> completion.
        /// </summary>
        public RaisableEvent onOutputLineCompleted;
        
        /// <summary>
        /// Callback to handle a parsed <see cref="Command"/>'s elements.
        /// </summary>
        public RaisableEvent<string[]> onHandleCommandRequested;

        /// <summary>
        /// Callback to output <see cref="DialogueOption">DialogueOptions</see>
        /// </summary>
        public RaisableEvent<DialogueOption[]> onOutputDialogueOptionsRequested;

        /// <summary>
        /// Callback on <see cref="onOutputLineRequested"/> completion. 
        /// </summary>
        public RaisableEvent onOutputDialogueOptionsCompleted; 

        /// <summary>
        /// Callback on setting a selected <see cref="DialogueOption"/> corresponding to its index.
        /// </summary>
        public RaisableEvent<int> onSetSelectedOption; 
        
        /// <summary>
        /// Summary callback on requesting a node by name (e.g. "Start").
        /// </summary>
        public RaisableEvent<string> onNodeRequested; 

        /// <summary>
        /// Callback when a node is entered.
        /// </summary>
        public RaisableEvent<string> onNodeStarted; 
        
        /// <summary>
        /// Callback when a node is completed.
        /// </summary>
        public RaisableEvent<string> onNodeCompleted;

        /// <summary>
        /// Callback on dialogue complete.
        /// </summary>
        public RaisableEvent onDialogueCompleted;

        /// <summary>
        /// Callback for all the lines that may run for this dialogue. 
        /// </summary>
        public RaisableEvent<IEnumerable<string>> onPrepareForLines; 
    }
}