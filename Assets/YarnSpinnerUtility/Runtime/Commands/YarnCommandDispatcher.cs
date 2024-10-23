using System;
using System.Linq;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace YarnSpinnerUtility.Runtime.Commands
{
    public class YarnCommandDispatcher: MonoBehaviour
    {
        [SerializeField] public DialogueParser dialogueParser;
        private readonly CommandDispatcher<string> commandDispatcher = new();
        
        private void OnEnable()
        {
            dialogueParser.OnCommandParsed += Dispatch;
        }

        private void OnDisable()
        {
            dialogueParser.OnCommandParsed -= Dispatch;
        }
        
        private async void Dispatch(Command command)
        {
            var commandElements = DialogueRunner.SplitCommandText(command.Text).ToArray();
            if (commandElements.Length == 0) return;
            var commandName = commandElements.ElementAtOrDefault(0);
            var commandArguments = commandElements.Length == 1 ? null : ParseValues(commandElements[1..]);

            if (commandName == "wait")
            {
                await Awaitable.WaitForSecondsAsync(Convert.ToSingle(commandElements[1]));
                dialogueParser.TryContinue();
                return;
            }
            
            commandDispatcher.TryDispatchCommand(commandName, commandArguments);
        }
        
        private static object[] ParseValues(string[] values)
        {
            var result = new object[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];
                result[i] = value switch
                {
                    _ when bool.TryParse(value, out var boolResult) => boolResult,
                    _ when float.TryParse(value, out var floatResult) => floatResult,
                    _ => value
                };
            }
    
            return result;
        }

        /// <summary>
        /// Adds a command handler. Dialogue will be paused at this point. 
        /// </summary>
        /// <param name="commandName">The name of the yarn command that is associated with the command.</param>
        /// <param name="commandHandler">The command that will be called.</param>
        public void AddCommandHandler(string commandName, Delegate commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);
        
        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler(string commandName, Action commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);
        
        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T>(string commandName, Action<T> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2>(string commandName, Action<T1, T2> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3>(string commandName, Action<T1, T2, T3> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4>(string commandName, Action<T1, T2, T3, T4> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5>(string commandName, Action<T1, T2, T3, T4, T5> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6>(string commandName, Action<T1, T2, T3, T4, T5, T6> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);

        /// <inheritdoc cref="AddCommandHandler(string, Delegate)"/>
        public void AddCommandHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string commandName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> commandHandler) => commandDispatcher.AddCommandHandler(commandName, commandHandler);
    }
}