using System;
using UnityEngine;

namespace Xomrac.Shmups
{

	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform _player;
		[SerializeField] private float _speed = 2f;

		private Transform _cachedTransform;

		private void Awake()
		{
			_cachedTransform = transform;
			var playerPosition = _player.position;
			_cachedTransform.position = new Vector3(playerPosition.x, playerPosition.y, _cachedTransform.position.z);
		}

		private void LateUpdate()
		{
			_cachedTransform.position += Vector3.up * (_speed * Time.deltaTime);
		}

	}

}