using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    public interface IDialogueOptionFactory
    {
        public GameObject CreateDialogueOption(DialogueOption dialogueOption);
        public void DestroyDialogueOption(GameObject dialogueOption);
    }
}