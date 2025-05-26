using Projectiles;

namespace Inventory.Items {
    public class Weapon {
        private readonly ProjectileData _data;

        public Weapon(ProjectileData data) {
            _data = data;
        }
        
        public void Shoot(IProjectileStrategy strategy) {
            Projectile projectile = ProjectilePool.Instance.Spawn(_data);
            strategy.SetupProjectile(projectile);
        }
    }
}