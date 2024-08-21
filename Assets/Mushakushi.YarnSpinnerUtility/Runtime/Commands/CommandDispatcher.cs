using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Commands
{
    public class CommandDispatcher<TKey>
    {
        private readonly Dictionary<TKey, Delegate> commandHandlers = new();

        public void DispatchCommand(TKey key, params object[] parameters)
        {
            if (!commandHandlers.TryGetValue(key, out var commandHandler))
            {
#if DEBUG
                Debug.LogError($"Command {key} is not registered");
#endif
                return;
            }

            if (!IsMethodCallableWithParameters(commandHandler.Method, parameters))
            {
#if DEBUG
                Debug.LogError($"Command {key} is not method callable with parameters {string.Join(", ", parameters)} ({commandHandler.Method.Name})");
#endif
                return;
            }
            
            commandHandler.DynamicInvoke(parameters);
        }

        public void AddCommandHandler(TKey key, Delegate commandHandler)
        {
            if (key == null) return;
            commandHandlers.TryAdd(key, commandHandler);
        }
        
        public void RemoveCommandHandler(TKey key)
        {
            if (key == null) return;
            commandHandlers.Remove(key);
        }
        
        /// <summary>
        /// Validates if the given method can be called with the specified parameters.
        /// </summary>
        /// <param name="method">The method info to validate.</param>
        /// <param name="parameters">The parameters to check against the method's parameter list.</param>
        /// <returns>True if the method can be called with the given parameters, otherwise false.</returns>
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