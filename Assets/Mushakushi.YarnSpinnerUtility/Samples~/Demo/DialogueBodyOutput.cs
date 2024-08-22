using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    [System.Serializable]
    public class DialogueBodyOutput: LineOutputTMPro
    {
        protected override string EvaluateText(LocalizedLine localizedLine) => localizedLine.TextWithoutCharacterName.Text;
    }
}