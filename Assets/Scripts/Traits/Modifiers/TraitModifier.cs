using System;
using Utils;

namespace Traits.Modifiers {
    public abstract class TraitModifier : IDisposable {
        public bool MarkedForRemoval { get; set; }
        public event Action<TraitModifier> OnDispose = delegate { };

        private readonly CountdownTimer _timer;

        protected TraitModifier(float duration) {
            if (duration <= 0) return;

            _timer = new CountdownTimer(duration);
            _timer.OnTimerStop += () => MarkedForRemoval = true;
            _timer.Start();
        }

        public abstract void Handle(object sender, Query query);

        public void Update(float delta) {
            _timer?.Tick(delta);
        }

        public void Dispose() {
            OnDispose.Invoke(this);
        }
    }
}