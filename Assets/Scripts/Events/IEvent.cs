namespace Events {
    public interface IEvent { }

    public interface ICancellable {
        public bool Cancelled { get; }
    }
}
