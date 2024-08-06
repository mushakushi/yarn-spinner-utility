using System;
using UnityEngine;

namespace Mushakushi.InkDialogueWriter.Runtime.Input.Default
{
    /// <summary>
    /// Default implementation using a string.
    /// </summary>
    [Serializable]
    public class DefaultDialogueStream: IDialogueStream
    {
        [SerializeField, TextArea] 
        private string dialogue;

        private int index;

        public bool IsEndOfStream => index >= dialogue.Length;
        
        public char Peek()
        {
            return dialogue[index];
        }

        public char Read()
        {
            var res = Peek();
            index++;
            return res;
        }

        public string ReadToEnd()
        {
            var res = dialogue[index..];
            index = dialogue.Length;
            return res;
        }

        public void Reset()
        {
            index = 0;
        }
    }
}