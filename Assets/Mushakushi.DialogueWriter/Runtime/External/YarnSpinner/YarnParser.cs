using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.DialogueWriter.Runtime.External.YarnSpinner
{
    public class YarnParser: MonoBehaviour
    {
        [SerializeField] private YarnObserver yarnObserver;
        [field: Header("Yarn"), SerializeField] public YarnProject Project { get; private set; }
        [field: SerializeField] public VariableStorageBehaviour VariableStorageBehaviour { get; private set; }
        [field: SerializeField] public LineProviderBehaviour LineProviderBehaviour { get; private set; }
        public Dialogue Dialogue { get; private set; }

        private void Awake()
        {
            Dialogue = CreateDialogueInstance();
            Dialogue.SetProgram(Project.Program);
            LineProviderBehaviour.YarnProject = Project;
        }
        
        private Dialogue CreateDialogueInstance()
        {
            return new Dialogue(VariableStorageBehaviour)
            {
#if DEBUG
                LogDebugMessage = message => yarnObserver.onDebugLog.RaiseEvent(this, message), 
                LogErrorMessage = message => yarnObserver.onDebugLogError.RaiseEvent(this, message),
#endif
                LineHandler = line => yarnObserver.onLineParsed.RaiseEvent(this, line),
                CommandHandler = command => yarnObserver.onCommandParsed.RaiseEvent(this, command),
                OptionsHandler = optionSet => yarnObserver.onOptionSetParsed.RaiseEvent(this, optionSet),
                NodeStartHandler = nodeName => yarnObserver.onNodeStarted.RaiseEvent(this, nodeName),
                NodeCompleteHandler = nodeName => yarnObserver.onNodeCompleted.RaiseEvent(this, nodeName),
                DialogueCompleteHandler = () => yarnObserver.onDialogueCompleted.RaiseEvent(this),
                PrepareForLinesHandler = lineIDs => yarnObserver.onPrepareForLines.RaiseEvent(this, lineIDs),
            };
        }
    }
}