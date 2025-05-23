using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Projectile {
    public class ProjectilePool : MonoBehaviour {
        public static ProjectilePool Instance { get; private set; }
        
        [SerializeField] private int _projectileCapacity;
        
        private readonly Dictionary<string, IObjectPool<Projectile>> _poolList;
        
        
        private IObjectPool<Projectile> GetPoolByName(ProjectileData data) {
            if (_poolList.TryGetValue(data.Owner, out var pool)) return pool;
            pool = new ObjectPool<Projectile>(
                createFunc: data.SummonProjectile,
                actionOnGet: data.ActionOnGetProjectile,
                actionOnDestroy: data.ActionOnDestroyProjectile,
                actionOnRelease: data.ActionOnReleaseProjectile,
                defaultCapacity: data.DefaultCapacity,
                maxSize: data.MaxCapacity,
                collectionCheck: false
            );
            _poolList.Add(data.Owner, pool);
            return pool;
        }
    }
}