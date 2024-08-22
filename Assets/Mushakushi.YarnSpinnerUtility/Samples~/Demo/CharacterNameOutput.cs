using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    [System.Serializable]
    public class CharacterNameOutput: LineOutputTMPro
    {
        // you could instead add a canvas group to show/hide if the character name is null or empty!
        
        protected override string EvaluateText(LocalizedLine localizedLine) => localizedLine.CharacterName;
    }
}