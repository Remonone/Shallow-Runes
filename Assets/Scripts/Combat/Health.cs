
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
        
        private void Awake() {
            _owner = GetComponent<Creature>();
            
        }

        private void UpdateMaxHealth(float newMaxHealth) {
            
        }

        
    }
}