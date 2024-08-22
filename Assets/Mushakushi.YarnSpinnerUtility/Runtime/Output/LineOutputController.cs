using System;
using System.Threading;
using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    public class LineOutputController: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeReference, SubclassSelector] private ILayoutElementAsync[] layoutElements;
        private CancellationTokenSource cancellationTokenSource = new();

        public void OnEnable()
        {
            dialogueObserver.lineParsed.OnEvent += HandleLineParsed;
            dialogueObserver.dialogueCompleted.OnEvent += HandleDialogueCompleted;
        }

        private void OnDisable()
        {
            dialogueObserver.lineParsed.OnEvent -= HandleLineParsed;
            dialogueObserver.dialogueCompleted.OnEvent -= HandleDialogueCompleted;
        }
        
        private void HandleLineParsed(LocalizedLine localizedLine)
        {
            RefreshTokenAndExecuteLayoutOperation(element => element.RecalculateLayout(localizedLine, cancellationTokenSource.Token));
        }
        
        private void HandleDialogueCompleted()
        {
            RefreshTokenAndExecuteLayoutOperation(element => element.RemoveLayout(cancellationTokenSource.Token));
        }

        private void RefreshTokenAndExecuteLayoutOperation(Func<ILayoutElementAsync, Awaitable> awaitable)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            
            _ = ExecuteLayoutOperation(awaitable);
        }

        private async Awaitable ExecuteLayoutOperation(Func<ILayoutElementAsync, Awaitable> awaitable)
        {
            try
            {
                foreach (var layoutElement in layoutElements)
                {
                    await awaitable(layoutElement);
                }
            }
            catch (OperationCanceledException) { }
        }
    }
}