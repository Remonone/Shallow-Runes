using Components;
using Events;
using Events.Types;
using Inventory;
using Inventory.Items;
using Projectiles;
using Projectiles.Strategy;
using UnityEngine;

namespace Creatures.Player {
    public class Player : Creature {

        [SerializeField] private InventoryStash _inventory;
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField] private CameraRotationComponent _rotation;
        [SerializeField] private GameObject _shootPosition;
        
        private Weapon _weapon;
        private PlayerProjectileStrategy _strategy;
        private Vector3 _direction;

        private bool _isShooting;
        
        public Vector3 Direction => _rotation.LookDirection;
        public Vector3 ShootPosition => _shootPosition.transform.position;

        private Listener<PlayerMainActionEvent> _mainActionListener;
        private Listener<PlayerSecondaryActionEvent> _secondaryActionListener;
        private Listener<PlayerAbilityActionEvent> _abilityActionListener;
        
        protected override void Awake() {
            base.Awake();
            
            _mainActionListener = new Listener<PlayerMainActionEvent>(OnMainAction);
            EventBus<PlayerMainActionEvent>.Register(_mainActionListener);
            _secondaryActionListener = new Listener<PlayerSecondaryActionEvent>(OnSecondaryAction);
            EventBus<PlayerSecondaryActionEvent>.Register(_secondaryActionListener);
            _abilityActionListener = new Listener<PlayerAbilityActionEvent>(OnAbilityAction);
            EventBus<PlayerAbilityActionEvent>.Register(_abilityActionListener);

            _strategy = new PlayerProjectileStrategy(this);
            _weapon = new Weapon(_projectileData);
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
            if (e.Released && !_isShooting) return;
            
            _isShooting = !e.Cancelled;
            _weapon.Shoot(_strategy);
        }
        
        private void OnSecondaryAction(PlayerSecondaryActionEvent e) {
            if (e.Cancelled) return;
            var fov = e.Pressed ? 40F : 60F;
            SetCameraViewEvent cameraViewEvent = new SetCameraViewEvent(fov);
            EventBus<SetCameraViewEvent>.Raise(cameraViewEvent);
        }

        private void OnAbilityAction(PlayerAbilityActionEvent e) {
            if (e.Cancelled) return;
        }
    }
}