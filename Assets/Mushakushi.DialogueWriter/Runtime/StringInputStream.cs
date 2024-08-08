using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime
{
    /// <summary>
    /// Default implementation using a string.
    /// </summary>
    [Serializable]
    public class StringInputStream: IInputStream
    {
        [SerializeField, TextArea] 
        private string dialogue;

        private int index;

        public bool IsEndOfStream => index >= dialogue.Length;

        public string Read()
        {
            var res = new string(dialogue[index], 1);
            index++;
            return res;
        }

        public async UniTask Open()
        {
            index = 0;
            await UniTask.CompletedTask;
        }

        public async UniTask Close()
        {
            await UniTask.CompletedTask;
        }
    }
}