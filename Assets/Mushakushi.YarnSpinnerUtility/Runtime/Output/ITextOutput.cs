namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    /// <summary>
    /// Outputs a string. 
    /// </summary>
    public interface ITextOutput
    {
        public void Open();
        public void Write(string text);
        public void Close();
    }
}