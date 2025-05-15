using Events;
using Events.Types;
using Inventory;
using UnityEngine;

namespace Creatures.Player {
    public class Player : Creature {

        [SerializeField] private InventoryStash _inventory;

        private Listener<PlayerMainActionEvent> _mainActionListener;
        private Listener<PlayerSecondaryActionEvent> _secondaryActionListener;
        private Listener<PlayerAbilityActionEvent> _abilityActionListener;
        
        protected override void Awake() {
            base.Awake();

            _inventory = GetComponent<InventoryStash>();
            
            _mainActionListener = new Listener<PlayerMainActionEvent>(OnMainAction);
            EventBus<PlayerMainActionEvent>.Register(_mainActionListener);
            _secondaryActionListener = new Listener<PlayerSecondaryActionEvent>(OnSecondaryAction);
            EventBus<PlayerSecondaryActionEvent>.Register(_secondaryActionListener);
            _abilityActionListener = new Listener<PlayerAbilityActionEvent>(OnAbilityAction);
            EventBus<PlayerAbilityActionEvent>.Register(_abilityActionListener);
        }

        private void OnDestroy() {
            EventBus<PlayerMainActionEvent>.Unregister(_mainActionListener);
            _mainActionListener.Remove(OnMainAction);
            EventBus<PlayerSecondaryActionEvent>.Unregister(_secondaryActionListener);
            _secondaryActionListener.Remove(OnSecondaryAction);
            EventBus<PlayerAbilityActionEvent>.Unregister(_abilityActionListener);
            _abilityActionListener.Remove(OnAbilityAction);
        }
        
        private void OnMainAction(PlayerMainActionEvent e) {
            if (e.Cancelled) return;
            _inventory.Weapon.MainAction();
        }
        
        private void OnSecondaryAction(PlayerSecondaryActionEvent e) {
            if (e.Cancelled) return;
            _inventory.Weapon.SecondaryAction();
        }

        private void OnAbilityAction(PlayerAbilityActionEvent e) {
            if (e.Cancelled) return;
        }
    }
}