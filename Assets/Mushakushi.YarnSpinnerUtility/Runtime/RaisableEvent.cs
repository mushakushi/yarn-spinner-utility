using System;

namespace Mushakushi.YarnSpinnerUtility.Runtime
{
    // (not so) fun fact: there is no better way of doing this 
    
    /// <summary>
    /// An event that can be externally invoked.
    /// </summary>
    public struct RaisableEvent
    {
        /// <summary>
        /// The event. 
        /// </summary>
        public event Action Event;

        /// <summary>
        /// Raises the <see cref="Event"/>.
        /// </summary>
        public void RaiseEvent() => Event?.Invoke();
    }
    
    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T obj) => Event?.Invoke(obj);
    }

    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T1, T2>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T1, T2> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T1 arg1, T2 arg2) => Event?.Invoke(arg1, arg2);
    }

    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T1, T2, T3>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T1, T2, T3> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T1 arg1, T2 arg2, T3 arg3) => Event?.Invoke(arg1, arg2, arg3);
    }
    
    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T1, T2, T3, T4>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T1, T2, T3, T4> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Event?.Invoke(arg1, arg2, arg3, arg4);
    }
    
    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T1, T2, T3, T4, T5>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T1, T2, T3, T4, T5> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Event?.Invoke(arg1, arg2, arg3, arg4, arg5);
    }
    
    /// <inheritdoc cref="RaisableEvent"/>
    public struct RaisableEvent<T1, T2, T3, T4, T5, T6>
    {
        /// <inheritdoc cref="RaisableEvent.Event"/>
        public event Action<T1, T2, T3, T4, T5, T6> Event;
        /// <inheritdoc cref="RaisableEvent.RaiseEvent"/>
        public void RaiseEvent(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => Event?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
    }
}