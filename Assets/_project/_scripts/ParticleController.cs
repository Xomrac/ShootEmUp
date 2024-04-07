using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Xomrac.Shmups.Projectile
{

	public class ParticleController : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _particleSystem;

		private Transform _cachedTransform;
		private Pool<ParticleController> _pool;

		private void Awake()
		{
			_cachedTransform = transform;
			if (_particleSystem == null)
			{
				_particleSystem = GetComponent<ParticleSystem>();
			}

			DestroyParticle();
		}

		private void DestroyParticle()
		{
			var duration = _particleSystem.main.duration;
			StartCoroutine(WaitAndDestroy(duration));
		}

		private IEnumerator WaitAndDestroy(float duration)
		{
			yield return new WaitForSeconds(duration);
			if (_pool != null)
			{
				_pool.Release(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void Setup(Vector3 newPosition)
		{
			_cachedTransform.position = newPosition;
		}

		public void SetPool(Pool<ParticleController> objectPool)
		{
			_pool = objectPool;
		}
	}

}