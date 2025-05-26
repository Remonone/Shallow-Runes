using UnityEngine;

namespace Projectiles {
    [CreateAssetMenu(menuName = "Shallow Runes/Data/Create Projectile Container", fileName = "Projectile")]
    public class ProjectileData : ScriptableObject {
        [SerializeField] public string ProjectileName;
        [SerializeField] public int DefaultCapacity;
        [SerializeField] public int MaxCapacity;

        [SerializeField] private GameObject _prefab;
        

        public Projectile SummonProjectile() {
            GameObject obj = Instantiate(_prefab);
            Projectile projectile = obj.GetComponent<Projectile>();
            projectile.Init(this);
            return projectile;
        }

        public void ActionOnGetProjectile(Projectile projectile) {
            projectile.gameObject.SetActive(true);
        }

        public void ActionOnDestroyProjectile(Projectile projectile) {
            Destroy(projectile.gameObject);
        }

        public void ActionOnReleaseProjectile(Projectile projectile) {
            projectile.Parameters.Clear();
            projectile.Direction = Vector3.zero;
            projectile.Origin = Vector3.zero;
            projectile.Range = float.MaxValue;
            projectile.Rigidbody.linearVelocity = Vector3.zero;
            projectile.gameObject.SetActive(false);
        }
    }
}