using UnityEngine;
using Xomrac.Shmups.Enemy;
using Xomrac.Shmups.Projectile;

namespace Xomrac.Shmups.Weapons
{

	[CreateAssetMenu(fileName = "MultiShotWeapon", menuName = "Weapons/Multi Shot Weapon")]
	public class MultiShotWeapon : WeaponBehaviour
	{
		[SerializeField] private ProjectileType _projectileType;

		
		[SerializeField] private int _shotsAmount;
		public int ShotsAmount => _shotsAmount;

		[SerializeField] private float _spreadAngle;
		public float SpreadAngle => _spreadAngle;

		public override void Fire(Transform firePoint, LayerMask layer)
		{
			PlayShotSound();
			float rotationStep = _shotsAmount > 1 ? _spreadAngle / (_shotsAmount - 1) : 0;
			float startAngle = _shotsAmount > 1 ? -_spreadAngle / 2 : 0;

			for (int i = 0; i < _shotsAmount; i++)
			{
				float rotation = startAngle + rotationStep * i;
				ProjectileController projectile = ProjectilesSpawner.Instance.SpawnProjectile(_projectileType, firePoint.position, firePoint.rotation);
				projectile.transform.forward = firePoint.up;
				projectile.transform.Rotate(0, 0, rotation,Space.World);
				projectile.gameObject.layer = layer;
				projectile.HandleLifeTime();
			}
		}
	}

}