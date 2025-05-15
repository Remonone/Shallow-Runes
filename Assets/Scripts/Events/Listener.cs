using System;

namespace Events {
    
    internal interface IEventListener<T> {
        /// <summary>
        /// Represents an event handler for a specific event type.
        /// The OnEvent property allows attaching an action that takes a single argument of type T to handle the event.
        /// </summary>
        public Action<T> OnEvent { get; set; }

        /// <summary>
        /// Represents an event binding with no arguments. This is used to subscribe to and trigger events that do not require any specific data.
        /// </summary>
        public Action OnEventNoArgs { get; set; }
    }
    
    public class Listener<T> : IEventListener<T> where T : IEvent {
        /// <summary>
        /// Represents an event binding with specified event handlers in C#.
        /// </summary>
        private Action<T> _onEvent = _ => {};

        /// <summary>
        /// Represents an event with no arguments in the Events namespace.
        /// </summary>
        private Action _onEventNoArgs = () => {};

        /// <summary>
        /// Represents a property that stores an action to be triggered when an event occurs.
        /// </summary>
        /// <value>
        /// An Action delegate that will be invoked when the event occurs.
        /// </value>
        Action<T> IEventListener<T>.OnEvent {
            get => _onEvent;
            set => _onEvent = value;
        }

        /// <summary>
        /// Represents an event handler without any arguments.
        /// Provides a mechanism to subscribe to and unsubscribe from the event.
        /// </summary>
        Action IEventListener<T>.OnEventNoArgs {
            get => _onEventNoArgs;
            set => _onEventNoArgs = value;
        }

        /// <summary>
        /// Represents a generic event binding that allows attaching and detaching event handlers.
        /// </summary>
        public Listener(Action<T> onEvent) => _onEvent = onEvent;

        /// <summary>
        /// Represents an event binding that associates actions with specific event types.
        /// </summary>
        public Listener(Action onEventNoArgs) => _onEventNoArgs = onEventNoArgs;

        /// <summary>
        /// Adds an event handler that takes no arguments to the <see cref="EventBinding"/>.
        /// </summary>
        /// <param name="onEvent">The action to be invoked when the event occurs.</param>
        public void Add(Action onEvent) => _onEventNoArgs += onEvent;

        /// <summary>
        /// Removes a specified action from the event binding for handling events with no arguments.
        /// </summary>
        /// <param name="onEvent">The action to be removed from the event binding.</param>
        public void Remove(Action onEvent) => _onEventNoArgs -= onEvent;

        /// <summary>
        /// Adds an action to be executed when the event occurs with arguments of type T.
        /// </summary>
        /// <param name="onEvent">The action to add to the event.</param>
        public void Add(Action<T> onEvent) => _onEvent += onEvent;

        /// <summary>
        /// Removes the specified action from the event binding.
        /// </summary>
        /// <param name="onEvent">The action to be removed from the event binding.</param>
        public void Remove(Action<T> onEvent) => _onEvent -= onEvent;
    }
}