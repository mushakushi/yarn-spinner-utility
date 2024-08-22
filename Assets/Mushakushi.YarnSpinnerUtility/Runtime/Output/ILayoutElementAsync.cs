using System.Threading;
using UnityEngine;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Runtime.Output
{
    public interface ILayoutElementAsync
    {
        public Awaitable RecalculateLayout(LocalizedLine localizedLine, CancellationToken cancellationToken);
        public Awaitable RemoveLayout(CancellationToken cancellationToken);
    }
}