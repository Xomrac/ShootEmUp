using System;
using UnityEngine;
using Xomrac.Shmups.Utilities.Attributes;
using Xomrac.Shmups.Utilities.Patterns;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;
using Xomrac.Shmups.Weapons;

namespace Xomrac.Shmups.Enteties
{

	public abstract class EntetiesWeapon : ServiceComponent<EntityController>
	{
		[SerializeField] protected WeaponBehaviour _weapon;
		[SerializeField] protected Transform _firePoint;
		[SerializeField,Layer] protected int _layer;

		protected override void Start()
		{
			base.Start();
			_weapon?.Initialize();
		}

		public void SetWeapon(WeaponBehaviour newWeapon)
		{
			_weapon = newWeapon;
			_weapon?.Initialize();
		}
	}

}