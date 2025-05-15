namespace Events.Types {
    public class PlayerLevelUpEvent : IEvent {
        public int OldLevel { get; }
        public int NewLevel { get; }

        public PlayerLevelUpEvent(int old, int @new) {
            OldLevel = old;
            NewLevel = @new;
        }
    }
}