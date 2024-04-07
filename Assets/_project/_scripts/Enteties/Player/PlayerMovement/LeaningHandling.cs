using System;
using UnityEngine;

namespace Xomrac.Shmups.Enteties.Player.Movement

{

	[Serializable]
	public class LeaningHandling
	{
		[SerializeField] private float _leanAngle = 15f;
		public float LeanAngle => _leanAngle;

		[SerializeField] private float _leanSpeed = 5f;
		public float LeanSpeed => _leanSpeed;
	}

}