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
    }
}