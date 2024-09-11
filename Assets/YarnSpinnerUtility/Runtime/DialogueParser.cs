using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace YarnSpinnerUtility.Runtime
{
    /// <summary>
    /// Parses a yarn yarnProject into dialogue that can be handled by other classes.
    /// </summary>
    /// <seealso href="https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs"/>
    public class DialogueParser: MonoBehaviour
    { 
        [SerializeField] private YarnProject yarnProject;
        [SerializeField] private VariableStorageBehaviour variableStorageBehaviour;
        [SerializeField] private LineProviderBehaviour lineProviderBehaviour;
        private Dialogue dialogue;
        
        /// <summary>
        /// Whether option selected is currently needed before the dialogue can be continued.
        /// </summary>
        public bool IsDialogueContinuable { get; private set; }
        
        /// <summary>
        /// Whether the dialogue has not yet completed.
        /// </summary>
        public bool IsDialogueRunning { get; private set; }

        /// <summary>
        /// Callback on dialogue started.
        /// </summary>
        public event Action OnDialogueStarted; 
        
        /// <summary>
        /// Callback on dialogue complete.
        /// </summary>
        public event Action OnDialogueCompleted;
        
        /// <summary>
        /// Callback on encountering a <see cref="LocalizedLine"/>.
        /// </summary>
        public event Action<LocalizedLine> OnLineParsed;
        
        /// <summary>
        /// Callback on encountering a parsed <see cref="Command"/>.
        /// </summary>
        /// <remarks>
        /// The first element 
        /// <see href="https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/f25cc05c40a6cdfcdb142248c9f6f35c8a40c157/Runtime/DialogueRunner.cs#L852">
        /// is always the command name</see>.
        /// </remarks>
        public event Action<Command> OnCommandParsed;

        /// <summary>
        /// Callback on encountering <see cref="DialogueOption">DialogueOptions</see>
        /// </summary>
        public event Action<DialogueOption[]> OnOptionsParsed;

        /// <summary>
        /// Callback on option selected. 
        /// </summary>
        public event Action<DialogueOption> OnOptionSelected; 

        /// <summary>
        /// Callback when a node is entered.
        /// </summary>
        public event Action<string> OnNodeStarted; 
        
        /// <summary>
        /// Callback when a node is completed.
        /// </summary>
        public event Action<string> OnNodeCompleted;

        /// <summary>
        /// Callback for all the lines that may run for this dialogue. 
        /// </summary>
        public event Action<IEnumerable<string>> OnLinesPrepared; 
        
#if DEBUG
        [SerializeField] private bool verboseLogging; 
#endif

        private void Awake()
        {
            dialogue = CreateDialogueInstance();
            dialogue.SetProgram(yarnProject.Program);
            lineProviderBehaviour.YarnProject = yarnProject;
            
            variableStorageBehaviour.SetAllVariables(yarnProject.InitialValues);
        }
        
        private Dialogue CreateDialogueInstance()
        {
            return new Dialogue(variableStorageBehaviour)
            {
#if DEBUG
                LogDebugMessage = message => { if (verboseLogging) Debug.Log(message, this); },
                LogErrorMessage = message => { if (verboseLogging) Debug.LogError(message, this); },
#endif
                LineHandler = HandleLine,
                CommandHandler = command => { OnCommandParsed?.Invoke(command); },
                OptionsHandler = HandleOptionSet,
                NodeStartHandler = nodeName => { OnNodeStarted?.Invoke(nodeName); },
                NodeCompleteHandler = nodeName => { OnNodeCompleted?.Invoke(nodeName); },
                DialogueCompleteHandler = HandleDialogueCompleted,
                PrepareForLinesHandler = HandlePrepareForLines,
            };
        }

        /// <summary>
        /// Prepares the <see cref="Dialogue"/> that the user intends to start running a node by name.
        /// </summary>
        /// <remarks>After this method is called, you call <see cref="TryContinue"/> to start executing it.</remarks>
        /// <seealso href="https://docs.yarnspinner.dev/api/csharp/yarn/yarn.dialogue/yarn.dialogue.setnode"/>
        public void SetNode(string nodeName)
        {
            if (!dialogue.NodeExists(nodeName)) return;
            
            IsDialogueContinuable = true;
            dialogue.SetNode(nodeName);
        }

        /// <summary>
        /// Tries to start, or continue, the execution of the current Program.
        /// </summary>
        /// <returns><c>true</c> if the dialogue could continue, <c>false</c> otherwise.</returns>
        /// <seealso href="https://docs.yarnspinner.dev/api/csharp/yarn/yarn.dialogue/yarn.dialogue.continue"/>
        public bool TryContinue()
        {
            if (!IsDialogueContinuable) return false;
            if (!IsDialogueRunning)
            {
                OnDialogueStarted?.Invoke();
                IsDialogueRunning = true;
            }
            dialogue.Continue();
            return true;
        }
        
        /// <summary>
        /// Signals to the Dialogue that the user has selected a specified Option.
        /// </summary>
        /// <param name="dialogueOption">The option that is selected. </param>
        /// <remarks>This method must be called before <see cref="TryContinue"/> can be called.</remarks>
        /// <seealso href="https://docs.yarnspinner.dev/api/csharp/yarn/yarn.dialogue/yarn.dialogue.setselectedoption"/>
        public void SetSelectedOption(DialogueOption dialogueOption)
        {
            IsDialogueContinuable = true;
            dialogue.SetSelectedOption(dialogueOption.DialogueOptionID);
            OnOptionSelected?.Invoke(dialogueOption);
        }

        private void HandleLine(Line line)
        {
            var localizedLine = lineProviderBehaviour.GetLocalizedLine(line);
            var text = Dialogue.ExpandSubstitutions(localizedLine.RawText, line.Substitutions);
            
            // https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/f25cc05c40a6cdfcdb142248c9f6f35c8a40c157/Runtime/DialogueRunner.cs#L921
            if (text == null)
            {
                Debug.LogWarning($"Dialogue Runner couldn't expand substitutions in Yarn Project [{ yarnProject.name }] node [{ dialogue.CurrentNode }] " 
                                 + $"with line ID [{ localizedLine.TextID }]. "
                                 + "This usually happens because it couldn't find text in the Localization. The line may not be tagged properly. "
                                 + "Try re-importing this Yarn Program. "
                                 + "For now, Dialogue Runner will swap in CurrentLine.RawText.");
                text = localizedLine.RawText;
            }
            
            dialogue.LanguageCode = lineProviderBehaviour.LocaleCode;
            localizedLine.Text = dialogue.ParseMarkup(text);
            
            OnLineParsed?.Invoke(localizedLine);
        }

        private void HandleOptionSet(OptionSet optionSet)
        {
            var dialogueOptions = new DialogueOption[optionSet.Options.Length];
            for (var i = 0; i < optionSet.Options.Length; i++)
            {
                var line = lineProviderBehaviour.GetLocalizedLine(optionSet.Options[i].Line);
                var text = Dialogue.ExpandSubstitutions(line.RawText, optionSet.Options[i].Line.Substitutions);
                dialogue.LanguageCode = lineProviderBehaviour.LocaleCode;
                line.Text = dialogue.ParseMarkup(text);

                dialogueOptions[i] = new DialogueOption
                {
                    TextID = optionSet.Options[i].Line.ID,
                    DialogueOptionID = optionSet.Options[i].ID,
                    Line = line,
                    IsAvailable = optionSet.Options[i].IsAvailable,
                };
            }
    
            IsDialogueContinuable = false;
            OnOptionsParsed?.Invoke(dialogueOptions);
        }
        
        

        private void HandleDialogueCompleted()
        {
            IsDialogueContinuable = false;
            IsDialogueRunning = false;
            OnDialogueCompleted?.Invoke();
        }

        private void HandlePrepareForLines(IEnumerable<string> lineIDs)
        {
            var lineIDsCopy = lineIDs as string[] ?? lineIDs.ToArray();
            lineProviderBehaviour.PrepareForLines(lineIDsCopy);
            
            OnLinesPrepared?.Invoke(lineIDsCopy);
        }
    }
}