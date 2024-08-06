namespace Mushakushi.InkDialogueWriter.Runtime.Input
{
    /// <summary>
    /// Provides dialogue. 
    /// </summary>
    public interface IDialogueStream
    {
        /// <summary>
        /// Whether the dialogue is fully read.
        /// </summary>
        public bool IsEndOfStream { get; }
        
        /// <summary>
        /// Gets the next character of a dialogue.
        /// </summary>
        public char Peek();
        
        /// <summary>
        /// Gets the next character of a dialogue and moves the iterator forward.
        /// </summary>
        public char Read();

        /// <summary>
        /// Gets the remaining characters in the dialogue. 
        /// </summary>
        public string ReadToEnd();

        /// <summary>
        /// Resets the stream, including the iterator. 
        /// </summary>
        public void Reset(); 
    }
}