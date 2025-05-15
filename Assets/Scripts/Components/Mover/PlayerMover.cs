using Constants;
using Creatures.Player;
using Events;
using Events.Types;
using UnityEngine;
using Utils;

namespace Components.Mover {
    public class PlayerMover : MonoBehaviour {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Cooldown _jumpCooldown;
        [SerializeField] private float _groundDrag;
        [SerializeField] private GameObject _playerModel;
        
        // Holds the force scale when the character is on slope.
        private static readonly float GROUND_HOLD_SCALE = 20F;
        
        private Rigidbody _rigidbody;
        private Vector3 _movingDirection;
        private float _maxSpeed;
        private Player _reference;

        private int Level => _reference.LevelComponent.Level;

        private State _state;

        private Listener<PlayerMoveEvent> _onPlayerMove;
        private Listener<PlayerJumpEvent> _onPlayerJump;
        private Listener<PlayerRunningEvent> _onPlayerRun;

        private Camera _playerCamera;

        #region Unity Functions

        private void Awake() {
            _onPlayerMove = new Listener<PlayerMoveEvent>(OnPlayerMove);
            EventBus<PlayerMoveEvent>.Register(_onPlayerMove);
            _onPlayerJump = new Listener<PlayerJumpEvent>(OnPlayerJump);
            EventBus<PlayerJumpEvent>.Register(_onPlayerJump);
            _onPlayerRun = new Listener<PlayerRunningEvent>(OnPlayerRun);
            EventBus<PlayerRunningEvent>.Register(_onPlayerRun);

            _state = new State(_groundMask);
            _playerCamera = Camera.main;
        }

        private void OnPlayerRun(PlayerRunningEvent e) {
            _state.Running = e.IsRunning;
        }

        private void OnDestroy() {
            EventBus<PlayerMoveEvent>.Unregister(_onPlayerMove);
            _onPlayerMove.Remove(OnPlayerMove);
            EventBus<PlayerJumpEvent>.Unregister(_onPlayerJump);
            _onPlayerJump.Remove(OnPlayerJump);
        }

        protected void Start() {
            if (!enabled) return;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = false;
            _reference = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _state.UpdatePlayerHeight(transform.position);
        }

        private void FixedUpdate() {
            _state.UpdateState(transform.position);
            var onSlope = _state.OnSlope;
            var direction = RotateMovingDirection();
            var speed = _maxSpeed * DataConstants.SPEED_SCALE;
            if (onSlope) {
                ProcessMovementOnSlope(direction, speed);
            } else {
                ProcessMovementOnGround(direction, speed);
            }

            _rigidbody.useGravity = !onSlope;
        }

        private void Update() {
            UpdateDrag();
            UpdateMaxSpeed();
            ClampSpeed();
            RotatePlayerTowardsDirection();
        }

        private void RotatePlayerTowardsDirection() {
            if (_rigidbody.linearVelocity.magnitude < .5F) return;
            _playerModel.transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity, Vector3.up);
        }

        #endregion

        #region EventHandlers

        private void OnPlayerJump() {
            if (!_state.Grounded && _jumpCooldown.IsActive) return;
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(Vector3.up * _reference.Traits.JumpForce(Level), ForceMode.Impulse);
        }
        
        private void OnPlayerMove(PlayerMoveEvent e) {
            _movingDirection = e.Direction.normalized;
        }

        #endregion

        #region Local Functions

        private void UpdateDrag() {
            _rigidbody.linearDamping = _state.Grounded || _state.OnSlope 
                ? _groundDrag : 0F;
        }
        
        private void UpdateMaxSpeed() {
            var speed = _reference.Traits.Speed(Level);
            if (_state.Running) {
                speed = _reference.Traits.RunningSpeed(Level);
            }
            _maxSpeed = speed;
        }
        
        private void ClampSpeed() {
            var speed = _maxSpeed * DataConstants.SPEED_SCALE;
            if (_state.OnSlope) {
                if (_rigidbody.linearVelocity.magnitude > speed) {
                    _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * speed;
                }
            }
            else {
                var horizontalVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
                if (horizontalVelocity.magnitude > speed) {
                    horizontalVelocity = horizontalVelocity.normalized * speed;
                }
                _rigidbody.linearVelocity =
                    new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z);
            }
        }
        
        private void ProcessMovementOnSlope(Vector3 direction, float speed) {
            _rigidbody.AddForce(GetSlopeMoveDirection(direction) * (speed * 1.5F), ForceMode.Force);
            if (_rigidbody.linearVelocity.y > 0) {
                _rigidbody.AddForce(Vector3.down * GROUND_HOLD_SCALE, ForceMode.Force);
            }
        }

        private void ProcessMovementOnGround(Vector3 direction, float speed) {
            if (_state.Grounded) {
                _rigidbody.AddForce(direction * speed, ForceMode.Force);
            }
            else {
                _rigidbody.AddForce(direction * (speed * DataConstants.AIRBORNE_SCALE));
            }
        }
        
        private Vector3 RotateMovingDirection() {
            var angle = _playerCamera.transform.rotation.eulerAngles.y;
            return Quaternion.Euler(0, angle, 0) * _movingDirection;
        }
        
        private Vector3 GetSlopeMoveDirection(Vector3 direction) {
            return Vector3.ProjectOnPlane(direction, _state.SlopeNormal).normalized;
        }

        #endregion
    }
}