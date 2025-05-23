using Events;
using Events.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.Player {
    public class InputController : MonoBehaviour {

        public void OnPlayerMove(InputAction.CallbackContext context) {
            var plainDirection = context.ReadValue<Vector2>();
            var normalisedDirection = (new Vector3(plainDirection.x, 0, plainDirection.y)).normalized;
            PlayerMoveEvent e = new PlayerMoveEvent(normalisedDirection);
            EventBus<PlayerMoveEvent>.Raise(e);
        }

        public void OnPlayerJump(InputAction.CallbackContext context) {
            if (!context.started) return;
            PlayerJumpEvent e = new PlayerJumpEvent();
            EventBus<PlayerJumpEvent>.Raise(e);
        }

        public void OnPlayerRun(InputAction.CallbackContext context) {
            if (context.started) return;
            PlayerRunningEvent e = new PlayerRunningEvent(context is { performed: true, canceled: false });
            EventBus<PlayerRunningEvent>.Raise(e);
        }

        public void OnPlayerLook(InputAction.CallbackContext context) {
            var mouseDirection = context.ReadValue<Vector2>();
            PlayerLookEvent e = new PlayerLookEvent(mouseDirection.normalized);
            EventBus<PlayerLookEvent>.Raise(e);
        }

        public void OnPlayerMainAction(InputAction.CallbackContext context) {
            if (context.started) return;
            PlayerMainActionEvent e = new PlayerMainActionEvent(context.performed, context.canceled);
            EventBus<PlayerMainActionEvent>.Raise(e);
        }

        public void OnPlayerSecondaryAction(InputAction.CallbackContext context) {
            if (context.started) return;
            PlayerSecondaryActionEvent e = new PlayerSecondaryActionEvent(context.performed, context.canceled);
            EventBus<PlayerSecondaryActionEvent>.Raise(e);
        }

        public void OnPlayerFirstAbilityAction(InputAction.CallbackContext context) {
            if (!context.started) return;
            PlayerAbilityActionEvent e =
                new PlayerAbilityActionEvent(PlayerAbilityActionEvent.Ability.FIRST);
            EventBus<PlayerAbilityActionEvent>.Raise(e);
        }
        
        public void OnPlayerSecondAbilityAction(InputAction.CallbackContext context) {
            if (!context.started) return;
            PlayerAbilityActionEvent e =
                new PlayerAbilityActionEvent(PlayerAbilityActionEvent.Ability.SECOND);
            EventBus<PlayerAbilityActionEvent>.Raise(e);
        }
        
        public void OnPlayerThirdAbilityAction(InputAction.CallbackContext context) {
            if (!context.started) return;
            PlayerAbilityActionEvent e =
                new PlayerAbilityActionEvent(PlayerAbilityActionEvent.Ability.THIRD);
            EventBus<PlayerAbilityActionEvent>.Raise(e);
        }

        public void OnPlayerInteraction(InputAction.CallbackContext context) {
            if (!context.started) return;
            PlayerInteractEvent e = new PlayerInteractEvent();
            EventBus<PlayerInteractEvent>.Raise(e);
        }
    }
}