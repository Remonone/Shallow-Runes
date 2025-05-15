using Events;
using Events.Types;
using UnityEngine;

namespace Components {
    public class CameraRotationComponent : MonoBehaviour {
        [SerializeField] private Vector2 _verticalClamp;

        private Listener<PlayerLookEvent> _lookEvent;

        private void Start() {
            _lookEvent = new Listener<PlayerLookEvent>(OnLook);
            EventBus<PlayerLookEvent>.Register(_lookEvent);
        }

        void OnLook(PlayerLookEvent e) {
            var direction = e.Delta.normalized * 5F;
            var eulerAngles = gameObject.transform.rotation.eulerAngles;
            eulerAngles.y = Mathf.Repeat(eulerAngles.y + direction.x + 180F, 360F) - 180F;
            eulerAngles.x = ClampAngle(eulerAngles.x + direction.y, _verticalClamp.x, _verticalClamp.y);
            transform.rotation = Quaternion.Euler(eulerAngles);
        }

        private float ClampAngle(float angle, float from, float to) {
            if (angle < 0F) angle += 360F;
            if (angle > 180F) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }
    }
}