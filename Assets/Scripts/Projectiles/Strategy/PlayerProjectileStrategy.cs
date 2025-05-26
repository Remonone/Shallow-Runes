using Creatures.Player;

namespace Projectiles.Strategy {
    public class PlayerProjectileStrategy : IProjectileStrategy {

        private readonly Player _player;

        public PlayerProjectileStrategy(Player player) {
            _player = player;
        }
        
        public void SetupProjectile(Projectile projectile) {
            projectile.Direction = _player.Direction;
            var speed = _player.Traits.AttackShotSpeed(_player.Level);
            projectile.Speed = speed;
            projectile.Origin = _player.ShootPosition;
            projectile.Range = _player.Traits.AttackRange(_player.Level);
            BuildProjectileParameters(projectile);
        }

        private void BuildProjectileParameters(Projectile projectile) {
            // TODO: Add projectile parameters based on matrix pattern
        }
    }
}