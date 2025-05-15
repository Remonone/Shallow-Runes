
namespace Events.Types {
    public class PlayerMainActionEvent : IEvent, ICancellable {
        public bool Cancelled { get; } = false;
    }

    public class PlayerSecondaryActionEvent : IEvent, ICancellable {
        public bool Cancelled { get; } = false;
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
}