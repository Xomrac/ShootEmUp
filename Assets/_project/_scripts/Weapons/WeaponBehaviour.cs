using UnityEngine;
using Xomrac.Shmups.Projectile;

namespace Xomrac.Shmups.Weapons
{

	public abstract class WeaponBehaviour : ScriptableObject
	{
		[SerializeField] private AudioData _soundEffect;
		public AudioData SoundEffect => _soundEffect;
		
		[SerializeField] protected float _fireRate;
		public float FireRate => _fireRate;


		public virtual void Initialize() { }

		protected void PlayShotSound()
		{
			SoundManager.Instance.PlayPlayerShot(_soundEffect);
		}
		
		public abstract void Fire(Transform firePoint,LayerMask layer);
	}
}