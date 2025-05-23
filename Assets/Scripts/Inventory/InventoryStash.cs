using Inventory.Items;
using UnityEngine;

namespace Inventory {
    public class InventoryStash : MonoBehaviour {
        private Weapon _weapon;

        public Weapon Weapon => _weapon;

        private void Awake() {
            _weapon = new Weapon();
        }
    }
}