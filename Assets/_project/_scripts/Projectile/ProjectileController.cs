using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Utilities.Extensions;

namespace Xomrac.Shmups.Projectile
{

	public class ProjectileController : MonoBehaviour
	{

		[Header("Model Settings")]
		[SerializeField] private MeshFilter _modelFilter;
		[SerializeField] private MeshRenderer _modelRenderer;

		
		[SerializeField] private float _movementSpeed = 10f;
		
		private Pool<ProjectileController> _pool;
		private Transform _cachedTransform;
		

		private float _damage;
		private float _lifeTime;
		public event Action OnUpdate;
		private Coroutine _lifeTimeCoroutine;

		public void SetSpeed(float speed) => _movementSpeed = speed;


		private void Awake()
		{
			_cachedTransform = transform;
		}

		private void Update()
		{
			_cachedTransform.position += _cachedTransform.forward * (_movementSpeed * Time.deltaTime);
			OnUpdate?.Invoke();
		}

		private void SpawnMuzzleEffect()
		{
			VFXSpawner.Instance.GetParticle(ParticleEffectType.Muzzle, _cachedTransform.position);
		}

		public void HandleLifeTime()
		{
			_lifeTimeCoroutine = StartCoroutine(WaitAndDestroy(_lifeTime));
		}

		private IEnumerator WaitAndDestroy(float time)
		{
			yield return new WaitForSeconds(time);
			VFXSpawner.Instance.GetParticle(ParticleEffectType.Explosion, _cachedTransform.position);
			_lifeTimeCoroutine = null;
			_pool.Release(this);
		}

		private void OnCollisionEnter(Collision other)
		{
			ContactPoint contact = other.contacts[0];
			VFXSpawner.Instance.GetParticle(ParticleEffectType.Impact,contact.point);
			var hitEntity = other.gameObject.GetComponent<EntityController>();
			if (hitEntity != null)
			{
				hitEntity.TryToGetHit(_damage);
			}
			StopCoroutine(_lifeTimeCoroutine);
			_pool.Release(this);
		}

		public void SetPool(Pool<ProjectileController> objectPool)
		{
			_pool = objectPool;
		}

		public void Initialize()
		{
		//	SpawnMuzzleEffect();
		}

		public void SetupProjectile(float damage, float speed, float lifeTime, Mesh mesh, Material material, Vector3 position, Quaternion rotation)
		{
			_lifeTime = lifeTime;
			_movementSpeed = speed;
			_damage = damage;
			_modelFilter.mesh = mesh;
			_modelRenderer.material = material;
			_cachedTransform.position = position;
			_cachedTransform.localRotation = rotation;
		}
	}

}