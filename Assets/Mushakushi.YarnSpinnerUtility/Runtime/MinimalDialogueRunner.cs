using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    /// <summary>
    /// Parses a yarn yarnProject into dialogue that can be handled by other classes. An improved version
    /// of https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs
    /// </summary>
    public class MinimalDialogueRunner: MonoBehaviour
    { 
        [SerializeField] private YarnDialogueObserver yarnDialogueObserver;
        [SerializeField] private YarnProject yarnProject;
        [SerializeField] private VariableStorageBehaviour variableStorageBehaviour;
        [SerializeField] private LineProviderBehaviour lineProviderBehaviour;
        private Dialogue dialogue;

        private void OnEnable()
        {
            yarnDialogueObserver.onNodeRequested.Event += dialogue.SetNode;
            yarnDialogueObserver.onContinueDialogue.Event += dialogue.Continue;
            yarnDialogueObserver.onSetSelectedOption.Event += dialogue.SetSelectedOption;
        }
        
        private void OnDisable()
        {
            yarnDialogueObserver.onNodeRequested.Event -= dialogue.SetNode;
            yarnDialogueObserver.onContinueDialogue.Event -= dialogue.Continue;
            yarnDialogueObserver.onSetSelectedOption.Event -= dialogue.SetSelectedOption;
        }

        private void Awake()
        {
            dialogue = CreateDialogueInstance();
            dialogue.SetProgram(yarnProject.Program);
            lineProviderBehaviour.YarnProject = yarnProject;
            
            variableStorageBehaviour.SetAllVariables(yarnProject.InitialValues);
        }
        
        /// <summary>
        /// https://docs.yarnspinner.dev/api/csharp/yarn/yarn.dialogue/yarn.dialogue.nodeexists
        /// </summary>
        public bool NodeExists(string nodeName) => dialogue.NodeExists(nodeName);
        
        private Dialogue CreateDialogueInstance()
        {
            return new Dialogue(variableStorageBehaviour)
            {
#if DEBUG
                LogDebugMessage = message => Debug.Log(message, this),
                LogErrorMessage = message => Debug.LogError(message, this),
#endif
                LineHandler = HandleLine,
                CommandHandler = HandleCommand, 
                OptionsHandler = HandleOptionSet,
                NodeStartHandler = yarnDialogueObserver.onNodeStarted.RaiseEvent,
                NodeCompleteHandler = yarnDialogueObserver.onNodeCompleted.RaiseEvent,
                DialogueCompleteHandler = yarnDialogueObserver.onDialogueCompleted.RaiseEvent,
                PrepareForLinesHandler = HandlePrepareForLines,
            };
        }

        private void HandleLine(Line line)
        {
            var localizedLine = lineProviderBehaviour.GetLocalizedLine(line);
            var text = Dialogue.ExpandSubstitutions(localizedLine.RawText, line.Substitutions);
            dialogue.LanguageCode = lineProviderBehaviour.LocaleCode;
            localizedLine.Text = dialogue.ParseMarkup(text);
            
            yarnDialogueObserver.onOutputLineRequested.RaiseEvent(localizedLine);
        }

        private void HandleCommand(Command command)
        {
            var commandElements = DialogueRunner.SplitCommandText(command.Text).ToArray(); 
            yarnDialogueObserver.onHandleCommandRequested.RaiseEvent(commandElements);
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
            
            yarnDialogueObserver.onOutputDialogueOptionsRequested.RaiseEvent(dialogueOptions);
        }

        private void HandlePrepareForLines(IEnumerable<string> lineIDs)
        {
            var lineIDsCopy = lineIDs as string[] ?? lineIDs.ToArray();
            lineProviderBehaviour.PrepareForLines(lineIDsCopy);
            
            yarnDialogueObserver.onPrepareForLines.RaiseEvent(lineIDsCopy);
        }
    }
}