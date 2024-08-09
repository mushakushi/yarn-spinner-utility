using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    public static class YarnUtility
    {
        /// <summary>
        /// Sets all initial values of a <see cref="VariableStorageBehaviour"/>.
        /// </summary>
        /// <param name="variableStorageBehaviour">The <see cref="VariableStorageBehaviour"/> to store the variables in.</param>
        /// <param name="variables">The <see cref="YarnProject.InitialValues"/>.</param>
        /// <param name="overrideExistingValues">Whether to override variables that are already set. </param>
        /// <seealso cref="VariableStorageBehaviour.SetAllVariables"/>
        // Modified from https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/f25cc05c40a6cdfcdb142248c9f6f35c8a40c157/Runtime/DialogueRunner.cs 
        public static void SetAllVariables(this VariableStorageBehaviour variableStorageBehaviour, Dictionary<string,IConvertible> variables, 
            bool overrideExistingValues = false)
        {
            foreach (var (key, value) in variables)
            {
                if (!overrideExistingValues && variableStorageBehaviour.Contains(key)) continue;

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (value.GetTypeCode())
                {
                    case TypeCode.String:
                        variableStorageBehaviour.SetValue(key, value.ToString(CultureInfo.InvariantCulture));
                        break;
                    case TypeCode.Boolean:
                        variableStorageBehaviour.SetValue(key, value.ToBoolean(CultureInfo.InvariantCulture));
                        break;
                    case TypeCode.Single:
                        variableStorageBehaviour.SetValue(key, value.ToSingle(CultureInfo.InvariantCulture));
                        break;
                    default:
#if DEBUG
                        Debug.LogWarning($"{key} is of an invalid type: {value.GetTypeCode()}");
#endif
                        break;
                }
            }
        }
        
        /// <summary>
        /// If the <see cref="commandElements"/> indicates a wait command, returns the seconds to wait, zero otherwise. 
        /// </summary>
        /// <remarks>
        /// You can use <see cref="Yarn.Unity.DialogueRunner.SplitCommandText"/> to obtain the command elements. 
        /// </remarks>
        public static float GetWaitCommandDuration(string[] commandElements)
        {
            if (commandElements[0] != "wait") return 0;
            if (commandElements.Length >= 2) return float.Parse(commandElements[1]);
#if DEBUG
            Debug.LogWarning("Asked to wait but given no duration!");
#endif
            return 0;
        }
    }
}