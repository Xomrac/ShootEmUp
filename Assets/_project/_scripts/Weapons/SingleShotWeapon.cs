using UnityEngine;
using Xomrac.Shmups.Enemy;
using Xomrac.Shmups.Projectile;

namespace Xomrac.Shmups.Weapons
{

	[CreateAssetMenu(fileName = "SingleShotWeapon", menuName = "Weapons/Single Shot Weapon")]
	public class SingleShotWeapon : WeaponBehaviour
	{
		[SerializeField] private ProjectileType _projectileType;

		
		public override void Fire(Transform firePoint, LayerMask layer)
		{
			PlayShotSound();
			ProjectileController projectile = ProjectilesSpawner.Instance.SpawnProjectile(_projectileType,firePoint.position, firePoint.rotation);
			projectile.transform.forward = firePoint.up;
			projectile.gameObject.layer = layer;
			projectile.HandleLifeTime();
		}
	}

}