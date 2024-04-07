using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Projectile;
using Xomrac.Shmups.Utilities.Patterns.Singleton;

namespace Xomrac.Shmups.Enemy
{

	public class EnemySpawner : Singleton<EnemySpawner>
	{
		[SerializeField] private EntityController _enemyPrefab;
		[SerializeField] private Transform _enemiesParent;
		[SerializeField] private int _poolDefaultSize = 10;
		[SerializeField] private int _poolMaxSize = 1000;
		[SerializeField] private bool _warmUpPoolOnStart = true;
		[SerializeField] private int _maxEnemies;

		

		[SerializeField] private List<EnemyType> _enemyTypes;
		[SerializeField] private List<SplineContainer> _paths;
		[SerializeField] private float _spawnRate = 2f;

		private EnemyFactory _enemyFactory;
		private Pool<EntityController> _enemiesPool;
		private int _currentEnemies;

		public Transform EnemiesParent => _enemiesParent;
		public EntityController EnemyPrefab => _enemyPrefab;
		private bool CanSpawnEnemy => _currentEnemies < _maxEnemies;

	#region Pooling

		private void DestroyEnemy(EntityController projectile)
		{
			Destroy(projectile.gameObject);
		}

		private void ReleaseEnemy(EntityController enemy)
		{
			_currentEnemies--;
			enemy.gameObject.SetActive(false);
			enemy.transform.localPosition = Vector3.zero;
		}

		private void GetEnemy(EntityController enemy)
		{
			_currentEnemies++;
			enemy.gameObject.SetActive(true);
		}

		private EntityController CreatePooledItem()
		{
			var newEnemy = _enemyFactory.CreatePooleableEnemy();
			newEnemy.SetPool(_enemiesPool);
			return newEnemy;
		}

	#endregion

		public override void Awake()
		{
			_enemyFactory = new EnemyFactory(this);
			_enemiesPool = new Pool<EntityController>
			(
				CreatePooledItem,
				GetEnemy,
				ReleaseEnemy,
				DestroyEnemy,
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
			StartCoroutine(SpawnEnemyCoroutine());
		}

		private void InitializeEntityPool()
		{
			var preparedEnemies = new List<EntityController>();
			for (int i = 0; i < _poolDefaultSize; i++)
			{
				preparedEnemies.Add(_enemiesPool.Get());
			}
			foreach (EntityController entityController in preparedEnemies)
			{
				_enemiesPool.Release(entityController);
			}
		}

		private IEnumerator SpawnEnemyCoroutine()
		{
			if (CanSpawnEnemy)
			{
				SpawnEnemy();
			}
			yield return new WaitForSeconds(_spawnRate);
			StartCoroutine(SpawnEnemyCoroutine());
		}

		private void SpawnEnemy()
		{
			EnemyType randomEnemyType = _enemyTypes[UnityEngine.Random.Range(0, _enemyTypes.Count)];
			SplineContainer randomPath = _paths[UnityEngine.Random.Range(0, _paths.Count)];

			EntityController newEnemy = _enemyFactory.CreateMovingEnemy(randomEnemyType, randomPath, _enemiesPool.Get());
		}

	}

}