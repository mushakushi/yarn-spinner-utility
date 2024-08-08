namespace Mushakushi.DialogueWriter.Runtime
{
    /// <summary>
    /// Provides dialogue. 
    /// </summary>
    public interface IInputStream: IAsyncStream
    {
        /// <summary>
        /// Gets the next character of a dialogue and moves the iterator forward.
        /// </summary>
        public string Read();
    }
}