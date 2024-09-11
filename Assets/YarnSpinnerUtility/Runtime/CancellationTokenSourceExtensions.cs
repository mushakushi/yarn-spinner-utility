using System;
using System.Threading;

namespace YarnSpinnerUtility.Runtime
{
    public static class CancellationTokenSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                cancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException) { }
            cancellationTokenSource.Dispose();
        }
    }
}