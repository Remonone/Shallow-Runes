using System;

namespace Utils.Attributes {
    [System.AttributeUsage(AttributeTargets.Field)]
    public class PriorityAttribute : Attribute {
        /// <summary>
        /// Indicates the priority level of an event
        /// </summary>
        public EventPriority EventPriority { get; private set; }
        
        /// <summary>
        /// Constructor for the PriorityAttribute class.
        /// </summary>
        /// <param name="eventPriority">The priority of the event.</param>
        public PriorityAttribute(EventPriority eventPriority) {
            EventPriority = eventPriority;
        }
    }
    public enum EventPriority {
        LOWEST = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3,
        HIGHEST = 4,
    }
    
}