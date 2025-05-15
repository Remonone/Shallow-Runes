
using System;
using Creatures.Player;
using Events;
using Events.Types;
using UnityEngine;

namespace Combat {
    public class Health : MonoBehaviour {
        private float _currentHealth;
        private float _maxHealth;
        private Creature _owner;
        
        private Listener<PlayerLevelUpEvent> _levelUpListener;

        private void Awake() {
            _levelUpListener = new Listener<PlayerLevelUpEvent>(OnLevelUp);
            EventBus<PlayerLevelUpEvent>.Register(_levelUpListener);
            _owner = GetComponent<Creature>();
            
        }
        
        private void OnDestroy() {
            EventBus<PlayerLevelUpEvent>.Unregister(_levelUpListener);
        }

        private void OnLevelUp(PlayerLevelUpEvent e) {
            var oldHealth = _maxHealth;
            _maxHealth = _owner.Traits.Health(e.NewLevel);
            _currentHealth = Math.Max(_maxHealth, _currentHealth + (oldHealth / _maxHealth) * _owner.Traits.HealthEfficiency(e.NewLevel));
        }
    }
}