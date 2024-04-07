using UnityEngine;
using Xomrac.Shmups.Projectile;

namespace Xomrac.Shmups.Enemy
{

	public class ProjectileBuilder
	{
		private ProjectileController _projectileObject;
		private float _damage;
		private float _speed;
		private Transform _parent;
		private Mesh _mesh;
		private Material _material;
		private Quaternion _rotation;
		private Vector3 _position;
		private float _lifeTime;

		public ProjectileBuilder(ProjectileController projectileObject = null)
		{
			_projectileObject = projectileObject;
		}
		
		public ProjectileController Build(ProjectileController basePrefab = null)
		{
			ProjectileController instance = _projectileObject != null ? _projectileObject : Object.Instantiate(basePrefab, _parent, true);
			instance.SetupProjectile(_damage, _speed,_lifeTime, _mesh, _material,_position, _rotation);
			return instance;
		}
		
		public ProjectileBuilder WithDamage(float damage)
		{
			_damage = damage;
			return this;
		}
		
		public ProjectileBuilder WithSpeed(float speed)
		{
			_speed = speed;
			return this;
		}
		
		public ProjectileBuilder ParentOf(Transform parent)
		{
			_parent = parent;
			return this;
		}
		
		public ProjectileBuilder WithMesh(Mesh mesh)
		{
			_mesh = mesh;
			return this;
		}
		
		public ProjectileBuilder WithLifeTime(float lifeTime)
		{
			_lifeTime = lifeTime;
			return this;
		}
		
		public ProjectileBuilder WithMaterial(Material material)
		{
			_material = material;
			return this;
		}

		public ProjectileBuilder WithDirection(Quaternion rotation)
		{
			_rotation = rotation;
			return this;
		}

		public ProjectileBuilder WithPosition(Vector3 position)
		{
			_position = position;
			return this;
		}
	}

}