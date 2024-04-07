using System;
using UnityEngine;

namespace Xomrac.Shmups.Enteties.Player.Movement

{

	[Serializable]
	public class Movementhandling
	{
		[SerializeField] private float _speed = 5f;
		public float Speed => _speed;

		[SerializeField] private float _smoothness = 0.1f;
		public float Smoothness => _smoothness;
	}

}