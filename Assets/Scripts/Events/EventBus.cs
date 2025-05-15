using System.Collections.Generic;
using System.Reflection;
using Utils.Attributes;

namespace Events {
    public static class EventBus<T> where T : IEvent {

        private static SortedSet<IEventListener<T>> _listeners = new(new PriorityComparator());

        public static void Register(Listener<T> listener) => _listeners.Add(listener);
        public static void Unregister(Listener<T> listener) => _listeners.Remove(listener);

        public static void Raise(T e) {
            foreach (var listener in _listeners) {
                listener.OnEvent.Invoke(e);
                listener.OnEventNoArgs.Invoke();
            }
        }

        public static void Clear() {
            _listeners.Clear();
        }
        
        /// <summary>
        /// Provides comparison logic for ordering and sorting events based on their priority.
        /// Events with a higher priority are considered greater than events with a lower priority.
        /// </summary>
        private class PriorityComparator : IComparer<IEventListener<T>> {
            
            /// <summary>
            /// Compares two events and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">First event to compare.</param>
            /// <param name="y">Second event to compare.</param>
            /// <returns>A signed integer that indicates the relative values of x and y</returns>
            public int Compare(IEventListener<T> x, IEventListener<T> y) {
                if (x == null) return -1;
                if (y == null) return 1;
                EventPriority xEventPriority = x.GetType().GetCustomAttribute<PriorityAttribute>()?.EventPriority ?? EventPriority.LOW;
                EventPriority yEventPriority = y.GetType().GetCustomAttribute<PriorityAttribute>()?.EventPriority ?? EventPriority.LOW;
                return xEventPriority - yEventPriority;
            }
        }
    }
    
    
}