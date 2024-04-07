using System;
using UnityEngine;

namespace Xomrac.Shmups
{
	[Serializable]
	public class LayerSettings
	{
		[SerializeField] private Transform _background;
		public Transform Background => _background;

		[SerializeField] private float _moveSpeed;
		public float MoveSpeed => _moveSpeed;

		
	}
	public class ParallaxController : MonoBehaviour
	{
		[SerializeField] private LayerSettings[] _layers;
		[SerializeField] private float _smoothing = 10f;

		private Transform _cameraTransform;
		private Vector3 _lastCameraPosition;

		private void Awake()
		{
			_cameraTransform= Camera.main.transform;
		}

		private void Start()
		{
			_lastCameraPosition = _cameraTransform.position;
		}

		private void Update()
		{
			foreach (LayerSettings layer in _layers)
			{
				var parallax = (_lastCameraPosition.y - _cameraTransform.position.y) * layer.MoveSpeed;
				var backgroundPosition = layer.Background.position;
				var targetY = backgroundPosition.y + parallax;
				var targetPosition = new Vector3(backgroundPosition.x, targetY, backgroundPosition.z);
				layer.Background.position = Vector3.Lerp(backgroundPosition, targetPosition, _smoothing * Time.deltaTime);
			}
			
			_lastCameraPosition = _cameraTransform.position;
		}
	}

}