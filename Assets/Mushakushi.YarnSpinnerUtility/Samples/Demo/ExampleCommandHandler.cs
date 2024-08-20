using Mushakushi.YarnSpinnerUtility.Runtime;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    public class ExampleCommandHandler: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;

        private void OnEnable()
        {
            dialogueObserver.commandParsed.OnEvent += HandleCommandParsed; 
        }

        private void OnDisable()
        {
            dialogueObserver.commandParsed.OnEvent -= HandleCommandParsed;
        }

        private static void HandleCommandParsed(string[] commandElements)
        {
            // you can handle commands here
            // the "wait" command is not handled by default
            // because the implementation may vary depending on usage of Awaitables, UniTask, Coroutines, etc
        }
    }
}