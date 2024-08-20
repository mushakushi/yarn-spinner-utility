using System.Collections;
using Mushakushi.YarnSpinnerUtility.Runtime;
using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    public class DialogueAutoAdvance: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private float waitTime = 0.5f;

        private void OnEnable()
        {
            dialogueObserver.dialogueCompleted.OnEvent += HandleDialogueCompleted;
            dialogueObserver.optionsParsed.OnEvent += HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent += HandleOptionSelected;
        }

        private void OnDisable()
        {
            dialogueObserver.dialogueCompleted.OnEvent -= HandleDialogueCompleted;
            dialogueObserver.optionsParsed.OnEvent -= HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent -= HandleOptionSelected;
        }

        private void HandleOptionsParsed(DialogueOption[] _) => StopAllCoroutines();

        private void HandleOptionSelected(int _) => StartCoroutine(_Continue());

        private void HandleDialogueCompleted() => StopAllCoroutines();

        private void Start()
        {
            dialogueObserver.nodeRequested.RaiseEvent("Start");
            dialogueObserver.dialogueStarted.RaiseEvent();
            StartCoroutine(_Continue());
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