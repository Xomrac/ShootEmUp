using UnityEngine;

namespace Xomrac.Shmups.Enteties
{

	public class EnemyShootingController : EntetiesWeapon
	{
		private float _fireTimer;

		private void Update()
		{
			if (_weapon==null) return;
			
			_fireTimer += Time.deltaTime;

			if (_fireTimer>= _weapon.FireRate)
			{
				_weapon.Fire(_firePoint,_layer);
				_fireTimer = 0;
			}
		}
	}

}