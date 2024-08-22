using System.Threading;
using System.Threading.Tasks;
using Mushakushi.YarnSpinnerUtility.Runtime.Output;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    public abstract class LineOutputTMPro: ILayoutElementAsync
    {
        // ReSharper disable once Unity.RedundantSerializeFieldAttribute
        [SerializeField] private TMP_Text text; 

        public async Awaitable RecalculateLayout(LocalizedLine localizedLine, CancellationToken _)
        {
            text.text = EvaluateText(localizedLine);
            await Task.CompletedTask;
            
            // alternatively, you could decide to implement a typing effect the text 
            // if this wasn't displaying text you could ignore the localized line and, instead, fade in a dialogue group.
        }

        protected abstract string EvaluateText(LocalizedLine localizedLine);

        public async Awaitable RemoveLayout(CancellationToken _)
        {
            await Task.CompletedTask;
            
            // you could perform some fancy effect with the text here when the dialogue is completed.
        }
    }
}