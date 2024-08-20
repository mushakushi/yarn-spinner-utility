using Mushakushi.YarnSpinnerUtility.Runtime.Output;
using TMPro;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    [System.Serializable]
    public class TextOutputTMPro: ITextOutput
    {
        [SerializeField] protected TMP_Text text;

        public void Open()
        {
            // does nothing, you could add a reference to a canvas group and show it here.
        }

        /// <summary>
        /// Sets the text immediately.  
        /// </summary>
        public virtual void Write(string value)
        {
            text.text = value;
        }

        public void Close()
        {
            // does nothing, you could add a reference to a canvas group and hide it here.
        }
    }
}