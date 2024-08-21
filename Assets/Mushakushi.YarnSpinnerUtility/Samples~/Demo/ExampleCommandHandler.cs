using Mushakushi.YarnSpinnerUtility.Runtime;
using Mushakushi.YarnSpinnerUtility.Runtime.Commands;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    public class ExampleCommandHandler: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private YarnCommandController commandController;

        private void Start()
        {
            // Please note that the "wait" command is not handled by default
            // because the implementation may vary depending on usage of Awaitables, UniTask, Coroutines, etc
            commandController.AddCommandHandler<string, string, string>("this", HandleRandomDemoCommand);
        }

        private void HandleRandomDemoCommand(string a, string b, string c)
        {
            Debug.Log($"Wow, th{a} {c} w{b}s handled!");
            dialogueObserver.commandHandled.RaiseEvent();
        }
    }
}