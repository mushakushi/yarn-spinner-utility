using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Serialization
{
    public static class VariableSerialization
    {        
        /// <summary>
        /// Saves the current state of dialogue variables to persistent storage as a JSON file.
        /// </summary>
        /// <param name="dialogueVariables">An instance of <see cref="DialogueVariables"/> containing the variables to save.</param>
        /// <param name="saveFileName">The name of the file to save the state to.</param>
        /// <returns>
        /// <c>true</c> if the save operation was successful; <c>false</c> if an error occurred.
        /// </returns>
        /// <remarks>
        /// The saved file will be located in the application's persistent data path.
        /// </remarks>
        public static bool SaveStateToPersistentStorage(DialogueVariables dialogueVariables, string saveFileName)
        {
            var data = SerializeAllVariablesToJson(dialogueVariables);
            var path = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                System.IO.File.WriteAllText(path, data);
                return true;
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.LogError($"Failed to save state to {path}: {e.Message}");
#endif
                return false;
            }
        }

        private static string SerializeAllVariablesToJson(DialogueVariables dialogueVariables)
        {
            var (floats, strings, booleans) = dialogueVariables;

            var data = new SaveDataJson
            {
                floatKeys = floats.Keys.ToArray(),
                floatValues = floats.Values.ToArray(),
                stringKeys = strings.Keys.ToArray(),
                stringValues = strings.Values.ToArray(),
                boolKeys = booleans.Keys.ToArray(),
                boolValues = booleans.Values.ToArray()
            };

            return JsonUtility.ToJson(data, true);
        }
        
        /// <summary>
        /// Deserializes a JSON string into three dictionaries containing float, string, and bool variables.
        /// This is intended to allow direct insertion of these variables into the variable storage.
        /// </summary>
        /// <param name="jsonData">A JSON string representing the variables to deserialize.</param>
        /// <returns>
        /// A <see cref="DeserializedVariables"/> object containing dictionaries for float, string, and bool variables.
        /// </returns>
        public static DeserializedVariables DeserializeAllVariablesFromJson(string jsonData)
        {
            var data = JsonUtility.FromJson<SaveDataJson>(jsonData);

            var floats = new Dictionary<string, float>();
            for (var i = 0; i < data.floatValues.Length; i++)
            {
                floats.Add(data.floatKeys[i], data.floatValues[i]);
            }

            var strings = new Dictionary<string, string>();
            for (var i = 0; i < data.stringValues.Length; i++)
            {
                strings.Add(data.stringKeys[i], data.stringValues[i]);
            }

            var booleans = new Dictionary<string, bool>();
            for (var i = 0; i < data.boolValues.Length; i++)
            {
                booleans.Add(data.boolKeys[i], data.boolValues[i]);
            }

            return new DeserializedVariables(floats, strings, booleans);
        }
        
        private struct SaveDataJson
        {
            public string[] floatKeys;
            public float[] floatValues;
            public string[] stringKeys;
            public string[] stringValues;
            public string[] boolKeys;
            public bool[] boolValues;
        }
    }
}
