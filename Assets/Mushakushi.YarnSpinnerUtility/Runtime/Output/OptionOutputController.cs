using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    public class OptionOutputController: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeReference, SubclassSelector] private IDialogueOptionFactory dialogueOptionFactory;
        private GameObject[] instantiatedDialogueOptions;

        private void OnEnable()
        {
            dialogueObserver.optionsParsed.OnEvent += HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent += HandleOptionSelected;
        }

        private void OnDisable()
        {
            dialogueObserver.optionsParsed.OnEvent -= HandleOptionsParsed;
            dialogueObserver.optionSelected.OnEvent -= HandleOptionSelected;
        }

        private void HandleOptionSelected(int _)
        {
            foreach (var instantiatedDialogueOption in instantiatedDialogueOptions)
            {
                dialogueOptionFactory.Dispose(instantiatedDialogueOption);
            }
        }

        private void HandleOptionsParsed(DialogueOption[] dialogueOptions)
        {
            instantiatedDialogueOptions = new GameObject[dialogueOptions.Length];
            for (var i = 0; i < dialogueOptions.Length; i++)
            {
                instantiatedDialogueOptions[i] = dialogueOptionFactory.Create(dialogueOptions[i]);
            }
        }
    }
}