using System;
using Events;
using Events.Types;
using UnityEngine;

namespace Components {
    public class LevelComponent : MonoBehaviour {

        [SerializeField] private float _range = 1.5F;
        [SerializeField] private int _shortage = 4;
        [SerializeField] private float _baseXp = 100F;
        
        public int Level { get; private set; }
        public float Experience { get; private set; }

        private float _experienceForLevelUp;

        private void Start() {
            Level = 1;
            Experience = 0;
            _experienceForLevelUp = CalculateRequiredExperience();
        }

        public void AddExperience(float experience) {
            Experience += experience;
            if (Experience > _experienceForLevelUp) {
                Experience = 0;
                PlayerLevelUpEvent e = new PlayerLevelUpEvent(Level, Level + 1);
                EventBus<PlayerLevelUpEvent>.Raise(e);
                ++Level;
                _experienceForLevelUp = CalculateRequiredExperience();
            }
        }
        
        private float CalculateRequiredExperience() {
            return (float)(_baseXp * Math.Pow(_range, (float)(Level - 1) / _shortage));
        }
    }
}