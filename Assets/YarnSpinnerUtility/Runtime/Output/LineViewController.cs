using Yarn.Unity;

namespace YarnSpinnerUtility.Runtime.Output
{
    public class LineViewController: ViewController<LocalizedLine>
    {
        public void OnEnable()
        {
            dialogueParser.OnDialogueStarted += HandleDialogueStarted;
            dialogueParser.OnLineParsed += HandleLineParsed;
            dialogueParser.OnDialogueCompleted += HandleDialogueCompleted;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dialogueParser.OnDialogueStarted -= HandleDialogueStarted;
            dialogueParser.OnLineParsed -= HandleLineParsed;
            dialogueParser.OnDialogueCompleted -= HandleDialogueCompleted;
        }

        private void HandleDialogueStarted()
        {
            InitializeAllViews();
        }

        private async void HandleLineParsed(LocalizedLine localizedLine)
        {
            await UpdateAllViews(localizedLine);
        }
        
        private void HandleDialogueCompleted()
        {
            ClearAllViews();
        }
    }
}