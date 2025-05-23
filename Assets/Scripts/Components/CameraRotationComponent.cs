using Events;
using Events.Types;
using Unity.Cinemachine;
using UnityEngine;

namespace Components {
    public class CameraRotationComponent : MonoBehaviour {
        [SerializeField] private CinemachineCamera _cameraAim;
        [SerializeField] private Vector2 _verticalClamp;

        private Listener<PlayerLookEvent> _lookEvent;
        private Listener<SetCameraViewEvent> _cameraViewEvent;
        private float _currentFieldOfView  = 60F;

        private void Start() {
            _lookEvent = new Listener<PlayerLookEvent>(OnLook);
            EventBus<PlayerLookEvent>.Register(_lookEvent);
            _cameraViewEvent = new Listener<SetCameraViewEvent>(OnCameraView);
            EventBus<SetCameraViewEvent>.Register(_cameraViewEvent);
        }

        private void OnCameraView(SetCameraViewEvent e) {
            _currentFieldOfView = e.FieldOfView;
        }

        private void Update() {
            var lens = _cameraAim.Lens.FieldOfView;
            _cameraAim.Lens.FieldOfView = Mathf.Lerp(lens, _currentFieldOfView, Time.deltaTime * 10f);
        }

        void OnLook(PlayerLookEvent e) {
            var direction = e.Delta.normalized * 1.5F;
            var eulerAngles = gameObject.transform.rotation.eulerAngles;
            eulerAngles.y = Mathf.Repeat(eulerAngles.y + direction.x + 180F, 360F) - 180F;
            eulerAngles.x = ClampAngle(eulerAngles.x - direction.y, _verticalClamp.x, _verticalClamp.y);
            transform.rotation = Quaternion.Euler(eulerAngles);
        }

        private float ClampAngle(float angle, float from, float to) {
            if (angle < 0F) angle += 360F;
            if (angle > 180F) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }
    }
}