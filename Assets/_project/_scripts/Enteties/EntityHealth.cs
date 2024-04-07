using System;
using UnityEngine;
using Xomrac.Shmups._project._scripts.Utilities.Patterns.Observer;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Utilities.Attributes;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;

namespace Xomrac.Shmups
{

	public class EntityHealth : ServiceComponent<EntityController>
	{
		[SerializeField] private float _maxHealth;
		[SerializeField, ReadOnly] private Observer<float> _currentHealth;
		private event EventHandler EntityDied;
		private AudioClip _deathSound;

		public float MaxHealth => _maxHealth;
		public Observer<float> CurrentHealth => _currentHealth;
		public float CurrentHealthPercentage => _currentHealth.Value / _maxHealth;
		public event EventHandler EntityDiedEvent
		{
			add => EntityDied += value;
			remove => EntityDied -= value;
		}
		
		protected override void Start()
		{
			base.Start();
			_currentHealth.Set(_maxHealth,false);
		}

		public void SetupHealth(int maxHealth)
		{
			_maxHealth = maxHealth;
			_currentHealth.Set(_maxHealth);
		}

		public void TakeDamage(float damage)
		{
			if (damage > 0)
			{
				ReduceHealth(damage);
			}
		}

		private void ReduceHealth(float amount)
		{
			_currentHealth.Value -= amount;
			if (_currentHealth.Value <= 0)
			{
				OnEntityDeath();
			}
		}

		private void OnEntityDeath()
		{
			Debug.Log("Entity should be dead", this);
			EntityDied?.Invoke(this, EventArgs.Empty);
			EntityDied = null;
		}
	}

}