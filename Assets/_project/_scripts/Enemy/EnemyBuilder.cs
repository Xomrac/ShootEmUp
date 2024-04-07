using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Utilities;
using Xomrac.Shmups.Weapons;

namespace Xomrac.Shmups.Enemy
{

	// A Builder class is a design pattern that simplifies the construction of complex objects step by step.
	// It allows to create objects with various configurations and options.
	// Providing a flexible and readable way to construct instances without the need for multiple constructors or overloaded methods.
	public class EnemyBuilder
	{
		private int _maxHealth = 10;
		private Sprite _sprite = null;
		private WeaponBehaviour _weapon = null;
		private SplineContainer _path = null;
		private float _pathPercentage = 0f;
		private float _movementSpeed = 1f;
		private Transform _parent = null;
		private EntityController _enemyObject = null;
		private SplineAnimate _splineAnimate = null;
		private int _scoreReward = 100;
		private AudioData _deathSound = null;

		public EnemyBuilder(EntityController enemyObject = null)
		{
			_enemyObject = enemyObject;
		}

		public EntityController Build(EntityController basePrefab = null)
		{
			EntityController instance = _enemyObject != null ? _enemyObject : Object.Instantiate(basePrefab, _parent, true);
			_splineAnimate = instance.GetOrAddComponent<SplineAnimate>();

			SetupSplineAnimate(instance);

			instance.SetupEntity(_sprite, _weapon,_maxHealth,_scoreReward,_deathSound);
			return instance;
		}

		private void SetupSplineAnimate(EntityController instance)
		{
			_splineAnimate.enabled = _path != null;
			if (_path == null) return;

			_splineAnimate.Container = _path;
			_splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
			_splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
			_splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.YAxis;
			_splineAnimate.MaxSpeed = _movementSpeed;
			_splineAnimate.NormalizedTime = _pathPercentage;
			instance.transform.position = _path.EvaluatePosition(_pathPercentage);
		}

		public EnemyBuilder WithSprite(Sprite sprite)
		{
			_sprite = sprite;
			return this;
		}

		public EnemyBuilder WithScoreReward(int scoreReward)
		{
			_scoreReward = scoreReward;
			return this;
		}
		public EnemyBuilder WithDeathSound(AudioData deathSound)
		{
			_deathSound = deathSound;
			return this;
		}

		public EnemyBuilder WithHealth(int health)
		{
			_maxHealth = health;
			return this;
		}

		public EnemyBuilder OnPath(SplineContainer path)
		{
			_path = path;
			return this;
		}

		public EnemyBuilder OnPathPoint(float pathPercentage)
		{
			_pathPercentage = Mathf.Clamp01(pathPercentage);
			return this;
		}

		public EnemyBuilder ParentOf(Transform parent)
		{
			_parent = parent;
			return this;
		}

		public EnemyBuilder WithWeapon(WeaponBehaviour weapon)
		{
			_weapon = weapon;
			return this;
		}

		public EnemyBuilder WithMovementSpeed(float movementSpeed)
		{
			_movementSpeed = movementSpeed;

			return this;
		}

	}

}