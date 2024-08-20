using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    /// <summary>
    /// Outputs dialogue body and character name output from a <see cref="LocalizedLine"/>.
    /// </summary>
    public class LineOutputController: MonoBehaviour
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private bool showCharacterName;
        [SerializeReference, SubclassSelector] private ITextOutput characterNameOutput;
        [SerializeReference, SubclassSelector] private ITextOutput dialogueBodyOutput;
        
        private void OnEnable()
        {
            dialogueObserver.lineParsed.OnEvent += OutputLine;
        }

        private void OnDisable()
        {
            dialogueObserver.lineParsed.OnEvent -= OutputLine;
        }
        
        private void OutputLine(LocalizedLine localizedLine)
        {
            var text = localizedLine.Text.Text;
            if (showCharacterName)
            {
                characterNameOutput?.Write(localizedLine.CharacterName);
                text = localizedLine.TextWithoutCharacterName.Text;
            }
            dialogueBodyOutput.Write(text);
        }
    }
}