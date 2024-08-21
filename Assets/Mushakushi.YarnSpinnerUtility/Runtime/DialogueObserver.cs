using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    [CreateAssetMenu(fileName = nameof(DialogueObserver), menuName = "Yarn Spinner/Utility/Dialogue Observer")]
    public class DialogueObserver : ScriptableObject
    {
        /// <summary>
        /// Callback on starting dialogue 
        /// </summary>
        /// <remarks>Useful to signify when the dialogue has been started for the first time.</remarks>
        /// <seealso cref="nodeStarted"/>
        public RaisableEvent dialogueStarted; 

        /// <summary>
        /// Callback on dialogue complete.
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent dialogueCompleted;
        
        /// <summary>
        /// Callback on continuing dialogue. 
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent continueRequested; 
        
        /// <summary>
        /// Callback on encountering a <see cref="LocalizedLine"/>.
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent<LocalizedLine> lineParsed;
        
        /// <summary>
        /// Callback on encountering a parsed <see cref="Command"/>.
        /// </summary>
        /// <remarks>
        /// Raised automatically by a <see cref="DialogueParser"/>. The first element 
        /// <see href="https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/f25cc05c40a6cdfcdb142248c9f6f35c8a40c157/Runtime/DialogueRunner.cs#L852">
        /// is always the command name</see>.
        /// </remarks>
        public RaisableEvent<Command> commandParsed;

        /// <summary>
        /// Callback on a command being handled. 
        /// </summary>
        /// <seealso cref="commandParsed"/>
        public RaisableEvent commandHandled; 

        /// <summary>
        /// Callback on encountering <see cref="DialogueOption">DialogueOptions</see>
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent<DialogueOption[]> optionsParsed;

        /// <summary>
        /// Callback on setting a selected <see cref="DialogueOption"/> corresponding to its index.
        /// </summary>
        public RaisableEvent<int> optionSelected; 
        
        /// <summary>
        /// Callback on requesting a node by name.
        /// </summary>
        public RaisableEvent<string> nodeRequested; 

        /// <summary>
        /// Callback when a node is entered.
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent<string> nodeStarted; 
        
        /// <summary>
        /// Callback when a node is completed.
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent<string> nodeCompleted;

        /// <summary>
        /// Callback for all the lines that may run for this dialogue. 
        /// </summary>
        /// <remarks>Raised automatically by a <see cref="DialogueParser"/>.</remarks>
        public RaisableEvent<IEnumerable<string>> linesPrepared; 
    }
}