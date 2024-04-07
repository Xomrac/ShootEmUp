using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Xomrac.Shmups.Enemy;
using Xomrac.Shmups.Projectile;
using Xomrac.Shmups.Utilities.Patterns.Singleton;

namespace Xomrac.Shmups
{

    public class ProjectilesSpawner : Singleton<ProjectilesSpawner>
    {
        [SerializeField] private ProjectileController _projectilePrefab;
        public ProjectileController ProjectilePrefab => _projectilePrefab;

        [SerializeField] private Transform _projectilesParent;
        public Transform ProjectilesParent => _projectilesParent;

        [SerializeField] private int _poolDefaultSize = 10;
        [SerializeField] private int _poolMaxSize = 1000;
        [SerializeField] private bool _warmUpPoolOnStart = true;

        private Pool<ProjectileController> _projectilesPool;

        private ProjectileFactory _factory;

        public override void Awake()
        {
            base.Awake();
            _factory = new ProjectileFactory(this);
            _projectilesPool = new Pool<ProjectileController>
            (
                CreatePooledItem,
                GetProjectile,
                ReleaseProjectile,
                DestroyProjectile,
                true,
                _poolDefaultSize,
                _poolMaxSize);
        }

        private void Start()
        {
            if (_warmUpPoolOnStart)
            {
                InitializeEntityPool();
            }
        }

        private void InitializeEntityPool()
        {
            var preparedProjectiles = new List<ProjectileController>();
            for (int i = 0; i < _poolDefaultSize; i++)
            {
                preparedProjectiles.Add(_projectilesPool.Get());
            }
            foreach (ProjectileController projectile in preparedProjectiles)
            {
                _projectilesPool.Release(projectile);
            }
        }

        private void DestroyProjectile(ProjectileController projectile)
        {
            Destroy(projectile.gameObject);
        }

        private void ReleaseProjectile(ProjectileController projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void GetProjectile(ProjectileController projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private ProjectileController CreatePooledItem()
        {
            var newProjectile = _factory.CreatePooleableProjectile();
            newProjectile.SetPool(_projectilesPool);
            return newProjectile;
        }

        public ProjectileController SpawnProjectile(ProjectileType projectileType,Vector3 position, Quaternion rotation)
        {
            ProjectileController newProjectile = _factory.CreateProjectile(projectileType,position,rotation, _projectilesPool.Get());

            newProjectile.Initialize();
            return newProjectile;
        }
    }
}
