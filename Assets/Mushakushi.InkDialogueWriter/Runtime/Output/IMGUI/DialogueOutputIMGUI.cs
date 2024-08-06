using System;
using Mushakushi.InkDialogueWriter.Runtime.Events;
using TMPro;
using UnityEngine;

namespace Mushakushi.InkDialogueWriter.Runtime.Output.IMGUI
{
    [Serializable]
    public class DialogueOutputIMGUI: IDialogueOutput
    {
        [SerializeField] 
        private DialogueEventChannel dialogueEventChannel;
        
        [SerializeField]
        private TMP_Text text; 
        
        [SerializeField] 
        private int maxLineCount;

        public bool IsEndOfOutput => text.textInfo.lineCount > maxLineCount;
        
        public void Open()
        {
            dialogueEventChannel.InvokeOnBindCompleted(this);
        }
    
        public void Write(string value)
        {
            text.text += value;
        }

        public void Clear()
        {
            text.text = string.Empty;
        }

        public void Close()
        {
            dialogueEventChannel.InvokeOnUnbindCompleted(this);
        }
    }
}