using UnityEngine;
using Xomrac.Shmups.Enemy;
using Xomrac.Shmups.Projectile;
using Xomrac.Shmups.Utilities.Extensions;

namespace Xomrac.Shmups.Weapons
{

	[CreateAssetMenu(fileName = "TrackingShotWeapon", menuName = "Weapons/Tracking Shot Weapon")]
	public class TrackingShotWeapon : WeaponBehaviour
	{
		[SerializeField] private ProjectileType _projectileType;

		
		[SerializeField] private float _trackingSpeed;

		public override void Fire(Transform firePoint, LayerMask layer)
		{
			PlayShotSound();
			var target = GameObject.FindGameObjectWithTag("Player").transform;
			ProjectileController projectile = ProjectilesSpawner.Instance.SpawnProjectile(_projectileType,firePoint.position, firePoint.rotation);

			SetupProjectile(projectile, firePoint, layer, target);
		}

		private void SetupProjectile(ProjectileController projectile, Transform firePoint, LayerMask layer, Transform target)
		{
			projectile.transform.forward = firePoint.up;
			projectile.gameObject.layer = layer;
			projectile.HandleLifeTime();
			
			if (target == null) return;
			
			projectile.OnUpdate += () => TrackTarget(projectile, target, firePoint);
		}

		private void TrackTarget(ProjectileController projectile, Transform target, Transform firePoint)
		{
			if (target==null)
			{
				return;
			}
			Vector3 direction = (target.position - projectile.transform.position).WithZ(firePoint.position.z).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.forward);
			projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, lookRotation, _trackingSpeed * Time.deltaTime);
		}
	}

}