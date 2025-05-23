using Events;
using Events.Types;
using UnityEngine;

namespace Inventory.Items {
    public class Weapon {

        public void Shoot(PlayerMainActionEvent e) {
            Debug.Log("Performing main action...");
        }
    }
}