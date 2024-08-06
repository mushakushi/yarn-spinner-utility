using Mushakushi.InkDialogueWriter.Runtime.Events;
using Mushakushi.InkDialogueWriter.Runtime.Input;
using Mushakushi.InkDialogueWriter.Runtime.Output;
using UnityEngine;

namespace Mushakushi.InkDialogueWriter.Runtime.Controller
{
    public class DialogueController: MonoBehaviour
    {
        [SerializeField]
        private DialogueEventChannel dialogueEventChannel;

        [SerializeReference, SubclassSelector] 
        private IDialogueStream dialogueStream;

        [SerializeReference, SubclassSelector] 
        private IDialogueOutput dialogueOutput;

        private DialogueIOBinding binding;

        private void Awake()
        {
            binding = new DialogueIOBinding(dialogueStream, dialogueOutput);
        }

        private void Start()
        {
            dialogueStream.Reset();
            dialogueOutput.Clear();
            dialogueEventChannel.InvokeOnBindStarted(binding);
            dialogueEventChannel.InvokeOnReadToEnd(binding);
        }
    }
}