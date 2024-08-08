using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime
{
    [Serializable]
    public class OutputStreamIMGUI: IOutputStream
    {
        [SerializeField]
        private TMP_Text text; 
        
        [SerializeField] 
        private int maxLineCount;

        public bool IsEndOfStream => text.textInfo.lineCount > maxLineCount;
        
        public async UniTask Open()
        {
            await UniTask.CompletedTask;
        }
    
        public void Write(string value)
        {
            text.text += value;
        }

        public void Clear()
        {
            text.text = string.Empty;
        }

        public async UniTask Close()
        {
            await UniTask.CompletedTask;
        }
    }
}