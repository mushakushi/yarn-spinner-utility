using System;
using System.Collections.Generic;
using System.Reflection;

namespace YarnSpinnerUtility.Runtime.Commands
{
    public class CommandDispatcher<TKey>
    {
        private readonly Dictionary<TKey, Delegate> commandHandlers = new();

        /// <summary>
        /// Attempts to dispatch a command with the specified key and parameters.
        /// </summary>
        /// <param name="key">The key that identifies the command handler to invoke.</param>
        /// <param name="parameters">The parameters to pass to the command handler.</param>
        /// <returns>
        /// <c>true</c> if the command was successfully dispatched; otherwise, <c>false</c>.
        /// </returns>
        public bool TryDispatchCommand(TKey key, params object[] parameters)
        {
            if (!commandHandlers.TryGetValue(key, out var commandHandler)) return false;
            if (!IsMethodCallableWithParameters(commandHandler.Method, parameters)) return false;
            commandHandler.DynamicInvoke(parameters);
            return true;
        }

        /// <summary>
        /// Adds a command handler for the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the command handler.</param>
        /// <param name="commandHandler">The command handler</param>
        public void AddCommandHandler(TKey key, Delegate commandHandler)
        {
            commandHandlers.TryAdd(key, commandHandler);
        }
        
        /// <summary>
        /// Removes the command handler associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the command handler to remove.</param>
        public void RemoveCommandHandler(TKey key)
        {
            commandHandlers.Remove(key);
        }
        
        /// <summary>
        /// Validates if the given method can be called with the specified parameters.
        /// </summary>
        /// <param name="method">The method info to validate.</param>
        /// <param name="parameters">The parameters to check against the method's parameter list.</param>
        /// <returns>True if the method can be called with the given parameters false otherwise.</returns>
        private static bool IsMethodCallableWithParameters(MethodInfo method, object[] parameters)
        {
            var methodParameters = method.GetParameters();
            if (methodParameters.Length != parameters.Length) return false; 
            
            for (var i = 0; i < methodParameters.Length; i++)
            {
                var expectedType = methodParameters[i].ParameterType;
                if (parameters[i] == null && expectedType.IsValueType && Nullable.GetUnderlyingType(expectedType) == null) return false;
                
                var parameterType = parameters[i].GetType();
                if (!expectedType.IsAssignableFrom(parameterType)) return false;
            }

            return true;
        }
    }
}