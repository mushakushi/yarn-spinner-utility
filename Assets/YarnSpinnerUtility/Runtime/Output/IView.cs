using System.Threading;
using UnityEngine;

namespace YarnSpinnerUtility.Runtime.Output
{
    public interface IView<in TArgs>
    {
        public void Initialize();
        public Awaitable Update(TArgs args, CancellationToken cancellationToken);
        public void Clear();
    }
}