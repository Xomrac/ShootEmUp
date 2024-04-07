using UnityEngine;
using Xomrac.Shmups.Projectile;

namespace Xomrac.Shmups.Enemy
{

	public class ProjectileFactory
	{
		private readonly ProjectilesSpawner _spawner;
		private readonly Transform _parent;

		public ProjectileFactory(ProjectilesSpawner spawner)
		{
			_spawner = spawner;
			_parent = spawner.ProjectilesParent;
		}
		private ProjectileBuilder PrepareProjectileBuilder(ProjectileType projectileType,ProjectileController projectile = null)
		{
			ProjectileBuilder builder = projectile == null ? new ProjectileBuilder() : new ProjectileBuilder(projectile);
			return builder
				.ParentOf(_parent)
				.WithDamage(projectileType.Damage)
				.WithSpeed(projectileType.MovementSpeed)
				.WithLifeTime(projectileType.LifeTime)
				.WithMesh(projectileType.ModelMesh)
				.WithMaterial(projectileType.ModelMaterial);
		}
		
		public ProjectileController CreateProjectile(ProjectileType projectileType, Vector3 position, Quaternion rotation, ProjectileController projectile = null)
		{
			ProjectileBuilder builder = PrepareProjectileBuilder(projectileType, projectile)
				.WithDirection(rotation)
				.WithPosition(position);

			return projectile == null ? builder.Build(_spawner.ProjectilePrefab) : builder.Build();

		}
	
		public ProjectileController CreatePooleableProjectile()
		{
			ProjectileBuilder builder = new ProjectileBuilder()
				.ParentOf(_parent);

			return builder.Build(_spawner.ProjectilePrefab);
		}
	}

}