using Creatures;
using Creatures.Player;
using UnityEngine;

namespace Components {
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