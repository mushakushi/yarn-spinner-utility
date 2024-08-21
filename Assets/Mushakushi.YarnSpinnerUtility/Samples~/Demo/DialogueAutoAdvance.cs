using System.Collections;
using Mushakushi.YarnSpinnerUtility.Runtime;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    public class DialogueAutoAdvance: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private float waitTime = 0.5f;
        private Coroutine autoAdvanceCoroutine;

        private void OnEnable()
        {
            dialogueObserver.dialogueCompleted.OnEvent += HandleDialogueCompleted;
            dialogueObserver.optionsParsed.OnEvent += HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent += HandleOptionSelected;
            dialogueObserver.commandParsed.OnEvent += HandleCommandParsed;
            dialogueObserver.commandHandled.OnEvent += HandleCommandHandled;
        }

        private void OnDisable()
        {
            dialogueObserver.dialogueCompleted.OnEvent -= HandleDialogueCompleted;
            dialogueObserver.optionsParsed.OnEvent -= HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent -= HandleOptionSelected;
            dialogueObserver.commandParsed.OnEvent -= HandleCommandParsed;
            dialogueObserver.commandHandled.OnEvent -= HandleCommandHandled;
        }

        private void HandleCommandHandled()
        {
            autoAdvanceCoroutine = StartCoroutine(_Continue());
        }

        private void HandleOptionSelected(int _)
        {
            autoAdvanceCoroutine = StartCoroutine(_Continue());
        }

        private void HandleCommandParsed(Command _) => StopCoroutine(autoAdvanceCoroutine);

        private void HandleOptionsParsed(DialogueOption[] _) => StopCoroutine(autoAdvanceCoroutine);

        private void HandleDialogueCompleted() => StopCoroutine(autoAdvanceCoroutine);

        private void Start()
        {
            dialogueObserver.nodeRequested.RaiseEvent("Start");
            dialogueObserver.dialogueStarted.RaiseEvent();
            autoAdvanceCoroutine = StartCoroutine(_Continue());
        }

        private IEnumerator _Continue()
        {
            while (true)
            {
                dialogueObserver.continueRequested.RaiseEvent();
                yield return new WaitForSeconds(waitTime);
            }
            
            // ReSharper disable once IteratorNeverReturns
        }
    }
}