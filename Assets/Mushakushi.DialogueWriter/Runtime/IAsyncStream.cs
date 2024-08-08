using Cysharp.Threading.Tasks;

namespace Mushakushi.DialogueWriter.Runtime
{
    /// <summary>
    /// A single stream of data.
    /// </summary>
    /// TODO - cancellation tokens
    public interface IAsyncStream
    {
        /// <summary>
        /// Whether this stream can be written or read to.
        /// </summary>
        public bool IsEndOfStream { get; }
        
        /// <summary>
        /// Open the dialogue output. 
        /// </summary>
        public UniTask Open();
        
        /// <summary>
        /// Closes the output. 
        /// </summary>
        public UniTask Close();
    }
}