using System.Collections.Generic;
using Projectiles;
using UnityEngine;

namespace Events.Types {
    public class ProjectileHitEvent : IEvent {
        public Projectile Projectile { get; }
        public Vector3 HitPoint { get; }
        public Vector3 Normal { get; }
        public GameObject Target { get; }

        public ProjectileHitEvent(Projectile projectile, GameObject target, Vector3 hitPoint, Vector3 normal) {
            Projectile = projectile;
            Target = target;
            HitPoint = hitPoint;
            Normal = normal;
        }
    }
}