namespace Mushakushi.DialogueWriter.Runtime
{
    public interface IOutputStream: IAsyncStream
    {
        
        /// <summary>
        /// Writes a string to the dialogue output.
        /// </summary>
        public void Write(string value);

        /// <summary>
        /// Clears the output.
        /// </summary>
        public void Clear();
    }
}