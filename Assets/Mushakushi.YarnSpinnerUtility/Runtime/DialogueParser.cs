using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    /// <summary>
    /// Parses a yarn yarnProject into dialogue that can be handled by other classes.
    /// </summary>
    /// <seealso href="https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs"/>
    public class DialogueParser: MonoBehaviour
    { 
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private YarnProject yarnProject;
        [SerializeField] private VariableStorageBehaviour variableStorageBehaviour;
        [SerializeField] private LineProviderBehaviour lineProviderBehaviour;
        private Dialogue dialogue;
        private bool canContinueDialogue; 
        
#if DEBUG
        [SerializeField] private bool verboseLogging; 
#endif

        private void OnEnable()
        {
            dialogueObserver.nodeRequested.OnEvent += HandleSetNode;
            dialogueObserver.continueRequested.OnEvent += HandleContinueRequested;
            dialogueObserver.optionSelected.OnEvent +=  HandleSetSelectedOption;
        }
        
        private void OnDisable()
        {
            dialogueObserver.nodeRequested.OnEvent -= HandleSetNode;
            dialogueObserver.continueRequested.OnEvent -= HandleContinueRequested;
            dialogueObserver.optionSelected.OnEvent -= HandleSetSelectedOption;
        }

        private void Awake()
        {
            canContinueDialogue = true;
            
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
                CommandHandler = command => { dialogueObserver.commandParsed.RaiseEvent(command); }, // will not pause dialogue using method group 
                OptionsHandler = HandleOptionSet,
                NodeStartHandler = dialogueObserver.nodeStarted.RaiseEvent,
                NodeCompleteHandler = dialogueObserver.nodeCompleted.RaiseEvent,
                DialogueCompleteHandler = dialogueObserver.dialogueCompleted.RaiseEvent,
                PrepareForLinesHandler = HandlePrepareForLines,
            };
        }

        private void HandleSetNode(string nodeName)
        {
            if (!dialogue.NodeExists(nodeName)) return;
            dialogue.SetNode(nodeName);
        }

        private void HandleContinueRequested()
        {
            if (!canContinueDialogue) return;
            dialogue.Continue();
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
            
            dialogueObserver.lineParsed.RaiseEvent(localizedLine);
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

            canContinueDialogue = false;
            dialogueObserver.optionsParsed.RaiseEvent(dialogueOptions);
        }

        private void HandleSetSelectedOption(int optionID)
        {
            canContinueDialogue = true;
            dialogue.SetSelectedOption(optionID);
        }

        private void HandlePrepareForLines(IEnumerable<string> lineIDs)
        {
            var lineIDsCopy = lineIDs as string[] ?? lineIDs.ToArray();
            lineProviderBehaviour.PrepareForLines(lineIDsCopy);
            
            dialogueObserver.linesPrepared.RaiseEvent(lineIDsCopy);
        }
    }
}