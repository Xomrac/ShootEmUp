using System;
using UnityEngine;

namespace Xomrac.Shmups
{

	[Serializable]
	public class CameraBounds
	{
		[SerializeField] private Transform _cameraTransform;
		public Transform CameraTransform => _cameraTransform;

		[SerializeField] private float _minX = -5f;
		public float MinX => _minX;

		[SerializeField] private float _maxX = 5f;
		public float MaxX => _maxX;

		[SerializeField] private float _minY = -1f;
		public float MinY => _minY;

		[SerializeField] private float _maxY = 1f;
		public float MaxY => _maxY;

		public float ClampX(float x)
		{
			float cameraPositionX = _cameraTransform.position.x;
			return Mathf.Clamp(x, cameraPositionX + _minX, cameraPositionX + _maxX);
		}

		public float ClampY(float y)
		{
			float cameraPositionY = _cameraTransform.position.y;
			return Mathf.Clamp(y, cameraPositionY + _minY, cameraPositionY + _maxY);
		}
	}

}