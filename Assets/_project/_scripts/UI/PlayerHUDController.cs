using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Xomrac.Shmups._project._scripts.UI
{

	public class PlayerHUDController : MonoBehaviour
	{
		[SerializeField] private Image _healthBar;
		[SerializeField] private EntityHealth _playerHealth;
		
		private void Start()
		{
			_playerHealth.CurrentHealth.AddListener(UpdateHealthBar);
		}


		private void UpdateHealthBar(float currentHealth)
		{
			float percentage = currentHealth / _playerHealth.MaxHealth;
			_healthBar.fillAmount = percentage;
		}

	}

}