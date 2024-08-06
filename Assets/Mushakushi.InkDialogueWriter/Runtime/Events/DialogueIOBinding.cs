using System;
using Mushakushi.InkDialogueWriter.Runtime.Input;
using Mushakushi.InkDialogueWriter.Runtime.Output;

namespace Mushakushi.InkDialogueWriter.Runtime.Events
{
    public readonly struct DialogueIOBinding
    {
        public readonly IDialogueStream stream;
        public readonly IDialogueOutput output;

        public DialogueIOBinding(IDialogueStream stream, IDialogueOutput output) : this()
        {
            this.stream = stream;
            this.output = output;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(stream, output);
        }
    }
}