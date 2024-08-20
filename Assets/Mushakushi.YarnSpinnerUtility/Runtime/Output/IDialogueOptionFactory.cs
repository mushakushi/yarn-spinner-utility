using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    public interface IDialogueOptionFactory
    {
        public GameObject Create(DialogueOption dialogueOption);
        public void Dispose(GameObject dialogueOption);
    }
}