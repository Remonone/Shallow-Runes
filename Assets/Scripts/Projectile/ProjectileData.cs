using UnityEngine;

namespace Projectile {
    public abstract class ProjectileData : ScriptableObject {

        public string Owner { get; }
        public int MaxCapacity { get; }
        public int DefaultCapacity { get; }

        public abstract Projectile SummonProjectile();
        public abstract void ActionOnGetProjectile(Projectile projectile);
        public abstract void ActionOnDestroyProjectile(Projectile projectile);
        public abstract void ActionOnReleaseProjectile(Projectile projectile);
    }
}