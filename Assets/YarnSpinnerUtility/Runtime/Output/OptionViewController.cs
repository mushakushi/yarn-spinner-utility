using Yarn.Unity;

namespace YarnSpinnerUtility.Runtime.Output
{
    public class OptionViewController: ViewController<DialogueOption[]>
    {
        private void OnEnable()
        {
            dialogueParser.OnOptionsParsed += HandleOptionsParsed;
            dialogueParser.OnOptionSelected += HandleOptionSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dialogueParser.OnOptionsParsed -= HandleOptionsParsed;
            dialogueParser.OnOptionSelected -= HandleOptionSelected;
        }
        
        private async void HandleOptionsParsed(DialogueOption[] dialogueOptions)
        {
            InitializeAllViews();
            await UpdateAllViews(dialogueOptions);
        }

        private void HandleOptionSelected(DialogueOption _)
        {
            ClearAllViews();
        }
    }
}