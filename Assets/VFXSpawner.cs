using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Xomrac.Shmups.Projectile;
using Xomrac.Shmups.Utilities.Patterns.Singleton;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Xomrac.Shmups
{

	public enum ParticleEffectType
	{
		Explosion,
		Impact,
		Muzzle
	}

	public class VFXSpawner : Singleton<VFXSpawner>
	{
		[SerializeField] private Transform _particlesParent;

		[SerializeField] private List<ParticleController> _explosionsEffects;
		[SerializeField] private List<ParticleController> _impactEffects;
		[SerializeField] private List<ParticleController> _muzzleEffects;

		[SerializeField] private int _poolsDefaultSize;
		[SerializeField] private int _poolsMaxSize;
		[SerializeField] private bool _warmUpPoolOnStart = true;
		private Dictionary<ParticleEffectType, Pool<ParticleController>> _particlePools;

		public override void Awake()
		{
			base.Awake();
			_particlePools = new Dictionary<ParticleEffectType, Pool<ParticleController>>
			{
				{ ParticleEffectType.Explosion, new Pool<ParticleController>(() => CreateParticle(ParticleEffectType.Explosion), OnGetParticle, OnReleaseParticle, DestroyParticle, true, _poolsDefaultSize, _poolsMaxSize) },
				{ ParticleEffectType.Impact, new Pool<ParticleController>(() => CreateParticle(ParticleEffectType.Impact), OnGetParticle, OnReleaseParticle, DestroyParticle, true, _poolsDefaultSize, _poolsMaxSize) },
				{ ParticleEffectType.Muzzle, new Pool<ParticleController>(() => CreateParticle(ParticleEffectType.Muzzle), OnGetParticle, OnReleaseParticle, DestroyParticle, true, _poolsDefaultSize, _poolsMaxSize) }
			};
			
		}

		private void Start()
		{
			if (!_warmUpPoolOnStart) return;
			
			InitializeEntityPool(ParticleEffectType.Explosion);
			InitializeEntityPool(ParticleEffectType.Impact);
			InitializeEntityPool(ParticleEffectType.Muzzle);
		}
		
		private void InitializeEntityPool(ParticleEffectType particleType)
		{
			var preparedParticles = new List<ParticleController>();
			for (int i = 0; i < _poolsDefaultSize; i++)
			{
				preparedParticles.Add(_particlePools[particleType].Get());
			}
			foreach (ParticleController particleController in preparedParticles)
			{
				_particlePools[particleType].Release(particleController);
			}
		}

		private void OnGetParticle(ParticleController particle)
		{
			particle.gameObject.SetActive(true);
		}

		private void DestroyParticle(ParticleController controller)
		{
			Destroy(controller.gameObject);
		}

		private void OnReleaseParticle(ParticleController particle)
		{
			particle.gameObject.SetActive(false);
		}

		private ParticleController GetRandomParticle(ParticleEffectType particleType)
		{
			return particleType switch
			{
				ParticleEffectType.Explosion => _explosionsEffects[Random.Range(0, _explosionsEffects.Count)],
				ParticleEffectType.Impact => _impactEffects[Random.Range(0, _impactEffects.Count)],
				ParticleEffectType.Muzzle => _muzzleEffects[Random.Range(0, _muzzleEffects.Count)],
				_ => null
			};
		}

		private ParticleController CreateParticle(ParticleEffectType particleType)
		{
			ParticleController randomParticle = GetRandomParticle(particleType);
			var newParticle = Instantiate(randomParticle, Vector3.zero, Quaternion.identity, _particlesParent);
			newParticle.SetPool(_particlePools[particleType]);
			return newParticle;
		}

		public ParticleController GetParticle(ParticleEffectType type,Vector3 position)
		{
			ParticleController particle = _particlePools[type].Get();
			particle.Setup(position);
			return particle;
		}
		
		public ParticleController GetParticle(ParticleController particleToSpawn,Vector3 position)
		{
			ParticleController particle = Instantiate(particleToSpawn, position, Quaternion.identity, _particlesParent);
			return particle;
		}

	}

}