using System;

namespace Utils {
    public abstract class Timer {
        protected float InitialTime;
        public float Time { get; set; }
        public bool IsRunning { get; protected set; }

        public float Progress => Time / InitialTime;
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value) {
            InitialTime = value;
            IsRunning = false;
        }

        public void Start() {
            Time = InitialTime;
            if (!IsRunning) {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);
    }

    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) {}
        
        public override void Tick(float deltaTime) {
            if (IsRunning) {
                Time -= deltaTime;
                if (Time <= 0) {
                    Time = 0;
                    IsRunning = false;
                    OnTimerStop.Invoke();
                }
            }
        }

        public bool IsFinished => Time <= 0;

        public void Reset() => Time = InitialTime;
    }
    
    public class StopwatchTimer : Timer {
        public StopwatchTimer() : base(0) { }

        public override void Tick(float deltaTime) {
            if (IsRunning) {
                Time += deltaTime;
            }
        }

        public void Reset() => Time = 0;

        public float GetTime() => Time;
    }
}