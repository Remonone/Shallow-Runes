using UnityEngine;

namespace Events.Types {
    public class PlayerMoveEvent : IEvent, ICancellable {
        public Vector3 Direction { get; }
        public bool Cancelled { get; }
        
        public PlayerMoveEvent(Vector3 direction) {
            Direction = direction;
            Cancelled = false;
        }
    }

    public class PlayerJumpEvent : IEvent, ICancellable {
        public bool Cancelled { get; } = false;
    }

    public class PlayerRunningEvent : IEvent {
        public bool IsRunning { get; }

        public PlayerRunningEvent(bool isRunning) {
            IsRunning = isRunning;
        }
    }

    public class PlayerLookEvent : IEvent, ICancellable {
        public Vector2 Delta { get; }
        public bool Cancelled { get; }

        public PlayerLookEvent(Vector2 delta) {
            Delta = delta;
            Cancelled = false;
        }
    }

    public class MoveCreatureEvent : IEvent, ICancellable {
        public Vector3 Position { get; }
        public bool Cancelled { get; }

        public MoveCreatureEvent(Vector3 position) {
            Position = position;
            Cancelled = false;
        }
    }
}