using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Weapons;

namespace Xomrac.Shmups.Enemy
{

	public class EnemyFactory
	{
		private readonly EnemySpawner _spawner;
		private readonly Transform _parent;

		public EnemyFactory(EnemySpawner spawner)
		{
			_spawner = spawner;
			_parent = spawner.EnemiesParent;
		}

		private EnemyBuilder PrepareEnemyBuilder(EnemyType enemyType, SplineContainer path, EntityController enemy = null)
		{
			EnemyBuilder builder = enemy == null ? new EnemyBuilder() : new EnemyBuilder(enemy);
			return builder
				.ParentOf(_parent)
				.WithScoreReward(enemyType.ScoreReward)
				.WithSprite(enemyType.Sprite)
				.WithWeapon(enemyType.Weapon)
				.WithHealth(enemyType.MaxHealth)
				.WithDeathSound(enemyType.DeathSound)
				.WithMovementSpeed(enemyType.MovementSpeed)
				.OnPath(path);
		}

		public EntityController CreateMovingEnemy(EnemyType enemyType, SplineContainer path, EntityController enemy = null)
		{
			EnemyBuilder builder = PrepareEnemyBuilder(enemyType, path, enemy)
				.WithWeapon(enemyType.Weapon);

			return enemy == null ? builder.Build(_spawner.EnemyPrefab) : builder.Build();
		}

		public EntityController CreateStaticEnemy(EnemyType enemyType, SplineContainer path, float pathPosition, EntityController enemy = null)
		{
			EnemyBuilder builder = PrepareEnemyBuilder(enemyType, path, enemy)
				.OnPathPoint(pathPosition)
				.WithMovementSpeed(0f);

			return enemy == null ? builder.Build(_spawner.EnemyPrefab) : builder.Build();
		}

		public EntityController CreateWeaponLessEnemy(EnemyType enemyType, SplineContainer path, EntityController enemy = null)
		{
			EnemyBuilder builder = PrepareEnemyBuilder(enemyType, path, enemy)
				.WithWeapon(null);

			return enemy == null ? builder.Build(_spawner.EnemyPrefab) : builder.Build();
		}

		public EntityController CreatePooleableEnemy()
		{
			EnemyBuilder builder = new EnemyBuilder()
				.ParentOf(_parent);

			return builder.Build(_spawner.EnemyPrefab);
		}
	}
}