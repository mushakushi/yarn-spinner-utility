namespace Mushakushi.InkDialogueWriter.Runtime.Output
{
    public interface IDialogueOutput
    {
        /// <summary>
        /// Whether more characters can be outputted without needing to
        /// <see cref="Clear"/>.
        /// </summary>
        public bool IsEndOfOutput { get; }
        
        /// <summary>
        /// Open the dialogue output. 
        /// </summary>
        public void Open();
        
        /// <summary>
        /// Writes a string to the dialogue output.
        /// </summary>
        public void Write(string value);

        /// <summary>
        /// Clears the output.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Closes the output. 
        /// </summary>
        public void Close();
    }
}