
namespace Events.Types {
    public class PlayerMainActionEvent : IEvent, ICancellable {
        public bool Cancelled { get; } = false;
        
        public bool Pressed { get; }
        public bool Released { get; }
        
        public PlayerMainActionEvent(bool pressed, bool released) {
            Pressed = pressed;
            Released = released;
        }
    }

    public class PlayerSecondaryActionEvent : IEvent, ICancellable {
        public bool Cancelled { get; } = false;
        public bool Pressed { get; }
        public bool Released { get; }
        
        public PlayerSecondaryActionEvent(bool pressed, bool released) {
            Pressed = pressed;
            Released = released;
        }
    }

    public class PlayerInteractEvent : IEvent { }

    public class PlayerAbilityActionEvent : IEvent, ICancellable {
        public enum Ability { FIRST, SECOND, THIRD }
        public Ability ChosenAbility { get; }
        public bool Cancelled { get; }
        
        public PlayerAbilityActionEvent(Ability ability) {
            ChosenAbility = ability;
            Cancelled = false;
        }
        
    }

    public class SetCameraViewEvent : IEvent {
        public float FieldOfView { get; }

        public SetCameraViewEvent(float fov) {
            FieldOfView = fov;
        }
    }
}