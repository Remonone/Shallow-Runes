using System.Collections.Generic;
using Events;
using Events.Types;
using UnityEngine;
using UnityEngine.Pool;

namespace Projectiles {
    public class ProjectilePool : MonoBehaviour {
        public static ProjectilePool Instance { get; private set; }
        
        private Dictionary<string, IObjectPool<Projectile>> _poolList;
        private Listener<UnloadSceneEvent> _onSceneUnload;
        
        private void Awake() {
            if (Instance != null) {
                Destroy(this);
                return;
            }

            Instance = this;
            _poolList = new Dictionary<string, IObjectPool<Projectile>>();
            
            _onSceneUnload = new Listener<UnloadSceneEvent>(ResetPools);
            EventBus<UnloadSceneEvent>.Register(_onSceneUnload);
        }

        private void ResetPools() {
            foreach (var row in _poolList) {
                row.Value.Clear();
            }
            _poolList.Clear();
        }

        public Projectile Spawn(ProjectileData data) {
            return GetPoolByDetails(data).Get();
        }

        public void Release(Projectile projectile) {
            GetPoolByDetails(projectile.Reference).Release(projectile);
        }
        
        private IObjectPool<Projectile> GetPoolByDetails(ProjectileData data) {
            if (_poolList.TryGetValue(data.ProjectileName, out var pool)) return pool;
            pool = new ObjectPool<Projectile>(
                createFunc: data.SummonProjectile,
                actionOnGet: data.ActionOnGetProjectile,
                actionOnDestroy: data.ActionOnDestroyProjectile,
                actionOnRelease: data.ActionOnReleaseProjectile,
                defaultCapacity: data.DefaultCapacity,
                maxSize: data.MaxCapacity,
                collectionCheck: false
            );
            _poolList.Add(data.ProjectileName, pool);
            return pool;
        }
    }
}