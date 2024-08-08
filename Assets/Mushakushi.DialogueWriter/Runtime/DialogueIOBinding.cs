using System;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime
{
    [Serializable]
    public struct DialogueIOBinding
    {
        [SerializeReference, SubclassSelector] public IInputStream inputStream;
        [SerializeReference, SubclassSelector] public IOutputStream outputStream;

        public DialogueIOBinding(IInputStream inputStream, IOutputStream outputStream) : this()
        {
            this.inputStream = inputStream;
            this.outputStream = outputStream;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(inputStream, outputStream);
        }
    }
}