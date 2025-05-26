using System.Collections.Generic;
using Creatures;
using Events;
using Events.Types;
using UnityEngine;

namespace Projectiles {
    [RequireComponent(typeof(MeshRenderer), typeof(Collider), typeof(Rigidbody))]
    public class Projectile : MonoBehaviour {

        
        private Vector3 _origin;
        
        internal float Speed;
        internal Vector3 Direction;
        internal Creature Owner;
        internal float Range;
        internal Rigidbody Rigidbody;

        internal Vector3 Origin {
            get => _origin;
            set {
                _origin = value;
                transform.position = value;
            }
        }

        internal Dictionary<string, object> Parameters = new();
        
        public ProjectileData Reference { get; private set; }

        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(ProjectileData data) {
            Reference = data;
        }

        private void OnCollisionEnter(Collision collision) {
            ContactPoint point = collision.contacts[0];
            ProjectileHitEvent e = new ProjectileHitEvent(this, collision.gameObject, point.point, point.normal);
            EventBus<ProjectileHitEvent>.Raise(e);
            ProjectilePool.Instance.Release(this);
        }

        private void FixedUpdate() {
            Rigidbody.AddForce(Direction * Speed, ForceMode.Force);
        }

        private void Update() {
            ClampSpeed();
        }
        
        
        private void ClampSpeed() {
            if (Rigidbody.linearVelocity.magnitude > Speed) {
                Rigidbody.linearVelocity = Rigidbody.linearVelocity.normalized * Speed;
            }
        }

        private void LateUpdate() {
            var distance = (transform.position - Origin).magnitude;
            if (distance > Range) {
                ProjectilePool.Instance.Release(this);
            }
        }
    }
}