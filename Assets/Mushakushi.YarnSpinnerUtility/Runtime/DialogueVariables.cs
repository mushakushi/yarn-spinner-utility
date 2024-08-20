using System;
using System.Collections.Generic;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    /// <summary>
    /// Return type alias for dialogue variables.  
    /// </summary>
    /// <seealso cref="Yarn.Unity.VariableStorageBehaviour.GetAllVariables"/>
    public abstract class DialogueVariables : Tuple<Dictionary<string, float>, Dictionary<string, string>, Dictionary<string, bool>>
    {
        protected DialogueVariables(Dictionary<string, float> item1, Dictionary<string, string> item2, Dictionary<string, bool> item3) : base(item1, item2, item3) { }
    }
}