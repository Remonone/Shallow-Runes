using System;
using UnityEngine;

namespace Utils {
    [Serializable]
    public class Cooldown {
        [Tooltip("Time in seconds")] 
        [SerializeField] private float _cooldown;

        private float _cooldownTime;

        public void SetCooldown(float newCooldown) {
            _cooldown = newCooldown;
        }

        public bool IsActive => _cooldownTime < Time.timeSinceLevelLoad;

        public void Reset() {
            _cooldownTime = Time.timeSinceLevelLoad + _cooldown;
        }
    }
}