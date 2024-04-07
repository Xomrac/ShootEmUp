using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Xomrac.Shmups.Utilities.Patterns;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;
using Xomrac.Shmups.Weapons;

namespace Xomrac.Shmups.Enteties
{

	public class EntityController : ServiceLocator
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		private Pool<EntityController> _pool;
		private AudioData _deathSound;

		private void OnEnable()
		{
			foreach (KeyValuePair<Type, ServiceComponent> valuePair in components)
			{
				Debug.Log($"Service {valuePair.Key} enabled", this);
			}
			var entityHealth = GetService<EntityHealth>();
			if (entityHealth != null)
			{
				Debug.Log("Entity health enabled", this);
				entityHealth.EntityDiedEvent += OnEntityDeath;
			}
		}

		private void OnEntityDeath(object _, EventArgs __)
		{
			Debug.Log("Entity died", this);
			GetEnabledService<EntityReward>()?.GiveReward();
			SoundManager.Instance.PlaySFX(_deathSound);
			_pool?.Release(this);
		}

		public void SetupEntity(Sprite sprite, WeaponBehaviour weapon, int maxHealth, int scoreReward, AudioData audioClip)
		{
			_spriteRenderer.sprite = sprite;
			_deathSound= audioClip;
			GetEnabledService<EntetiesWeapon>()?.SetWeapon(weapon);
			GetEnabledService<EnemyShootingController>()?.SetWeapon(weapon);
			GetEnabledService<EntityHealth>()?.SetupHealth(maxHealth);
			GetEnabledService<EntityReward>()?.SetReward(scoreReward);
		}

		public void SetPool(Pool<EntityController> enemiesPool)
		{
			_pool = enemiesPool;
		}

		public void TryToGetHit(float damage)
		{
			GetEnabledService<EntityHealth>()?.TakeDamage(damage);
		}
	}

}