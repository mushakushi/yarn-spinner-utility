using System.Collections.Generic;

namespace YarnSpinnerUtility.Runtime.Serialization
{
    /// <summary>
    /// Deserialized variables from a JSON string.
    /// </summary>
    public readonly struct DeserializedVariables
    {
        /// <summary>
        /// A dictionary containing the float variables.
        /// </summary>
        public Dictionary<string, float> FloatVariables { get; }

        /// <summary>
        /// A dictionary containing the string variables.
        /// </summary>
        public Dictionary<string, string> StringVariables { get; }

        /// <summary>
        /// A dictionary containing the bool variables.
        /// </summary>
        public Dictionary<string, bool> BoolVariables { get; }
        
        public DeserializedVariables(Dictionary<string, float> floatVariables, Dictionary<string, string> stringVariables, Dictionary<string, bool> boolVariables)
        {
            FloatVariables = floatVariables;
            StringVariables = stringVariables;
            BoolVariables = boolVariables;
        }
    }
}