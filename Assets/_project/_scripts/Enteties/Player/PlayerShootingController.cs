using UnityEngine;
using Xomrac.Shmups.Enteties.Player;

namespace Xomrac.Shmups.Enteties
{

	public class PlayerShootingController : EntetiesWeapon
	{
		private InputReader _inputs;
		private float _fireTimer;

		protected override void Start()
		{
			base.Start();
			_inputs = ServiceLocator.GetService<InputReader>();
			enabled = _inputs != null;
		}
		private void Update()
		{
			if (_weapon==null) return;
			
			_fireTimer += Time.deltaTime;

			if (_inputs.Fire && _fireTimer>= _weapon.FireRate)
			{
				_weapon.Fire(_firePoint,_layer);
				_fireTimer = 0;
			}
		}
	}

}