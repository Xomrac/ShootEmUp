using UnityEngine;
using Xomrac.Shmups.Weapons;

namespace Xomrac.Shmups.Enemy
{

	[CreateAssetMenu(fileName = "EnemyType", menuName = "Data/Enemy/Enemy Type")]
	public class EnemyType : ScriptableObject
	{
		[SerializeField] private int _maxHealth = 100;
		public int MaxHealth => _maxHealth;

		[SerializeField] private Sprite _sprite;
		public Sprite Sprite => _sprite;

		[SerializeField] private WeaponBehaviour _weapon;
		public WeaponBehaviour Weapon => _weapon;

		[SerializeField] private float _movementSpeed = 5f;
		public float MovementSpeed => _movementSpeed;

		[SerializeField] private int _scoreReward = 100;
		public int ScoreReward => _scoreReward;

		[SerializeField] private AudioData _deathSound;
		public AudioData DeathSound => _deathSound;

	}

}