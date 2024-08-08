using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Mushakushi.DialogueWriter.Runtime.External.YarnSpinner
{
    /// <summary>
    /// Evaluates a <see cref="Yarn.Command"/> to a function and automatically handles the "wait" command,
    /// delegating any other commands to other scripts. 
    /// </summary>
    public class YarnCommandParser: MonoBehaviour
    {
        [SerializeField] private YarnObserver yarnObserver;

        private void OnEnable()
        {
            yarnObserver.onCommandParsed.Event += HandleOnCommandParsed;
        }
        
        private void OnDisable()
        {
            yarnObserver.onCommandParsed.Event -= HandleOnCommandParsed;
        }

        /// <summary>
        /// Parses the <see cref="Yarn.Command"/>
        /// </summary>
        /// <param name="parser">The input stream where this command was encountered.</param>
        /// <param name="command">The command.</param>
        private async void HandleOnCommandParsed(YarnParser parser, Yarn.Command command)
        {
            // yes I (also) do see the irony in using the full dialogue runner to make a minimal one
            var elements = Yarn.Unity.DialogueRunner.SplitCommandText(command.Text).ToArray();

            if (elements[0] == "wait")
            {
                if (elements.Length < 2)
                {
#if DEBUG
                    Debug.LogWarning("Asked to wait but given no duration!");
#endif
                    return;
                }
                var seconds = float.Parse(elements[1]);
                if (seconds <= 0) return;

                // parser.IsPaused = true;
                await UniTask.Delay(TimeSpan.FromSeconds(seconds), ignoreTimeScale: false); 
                // parser.IsPaused = false;
            }
            else
            {
                commandHandler?.HandleCommand(elements);
            }
        }
    }
}