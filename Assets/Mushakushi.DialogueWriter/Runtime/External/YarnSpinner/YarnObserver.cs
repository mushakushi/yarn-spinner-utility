using System.Collections.Generic;
using UnityEngine;
using Yarn;

namespace Mushakushi.DialogueWriter.Runtime.External.YarnSpinner
{
    [CreateAssetMenu(fileName = nameof(YarnObserver), menuName = "ScriptableObjects/Yarn/Yarn Observer")]
    public class YarnObserver : ScriptableObject
    {
        public RaisableEvent onPause; 
        
        /// <summary>
        /// Callback on logging a normal message. 
        /// </summary>
        public RaisableEvent<YarnParser, string> onDebugLog;

        /// <summary>
        /// Callback on logging an error message. 
        /// </summary>
        public RaisableEvent<YarnParser, string> onDebugLogError;
        
        /// <summary>
        /// Callback on yarn line read. 
        /// </summary>
        public RaisableEvent<YarnParser, Line> onLineParsed;
        
        /// <summary>
        /// Callback on yarn command read. 
        /// </summary>
        public RaisableEvent<YarnParser, Command> onCommandParsed;

        /// <summary>
        /// Callback on option set read. 
        /// </summary>
        public RaisableEvent<YarnParser, OptionSet> onOptionSetParsed;

        /// <summary>
        /// Callback when a node is entered.
        /// </summary>
        public RaisableEvent<YarnParser, string> onNodeStarted; 
        
        /// <summary>
        /// Callback when a node is completed.
        /// </summary>
        public RaisableEvent<YarnParser, string> onNodeCompleted;

        /// <summary>
        /// Callback on dialogue complete.
        /// </summary>
        public RaisableEvent<YarnParser> onDialogueCompleted;

        /// <summary>
        /// Callback for all the lines that may run for this dialogue. 
        /// </summary>
        public RaisableEvent<YarnParser, IEnumerable<string>> onPrepareForLines; 
        
        public RaisableEvent<
    }
}