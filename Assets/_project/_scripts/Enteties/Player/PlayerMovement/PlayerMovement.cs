using UnityEngine;
using Xomrac.Shmups.Utilities.Patterns;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;

namespace Xomrac.Shmups.Enteties.Player.Movement
{

	public class PlayerMovement : ServiceComponent<EntityController>
	{
		[Header("Elements")] [SerializeField] private GameObject _model;

		[Space(10)] [Header("Settings")] [SerializeField]
		private Movementhandling _movementHandler;
		[Space] [SerializeField] private LeaningHandling _leaningHandler;
		[Space] [SerializeField] private CameraBounds _cameraBounds;

		private Vector3 _currentVelocity;
		private Vector3 _targetPosition;
		private Transform _cachedTransform;
		private InputReader _inputs;

		protected override void Awake()
		{
			base.Awake();
			_cachedTransform = transform;
		}

		protected override void Start()
		{
			base.Start();
			_inputs = ServiceLocator.GetService<InputReader>();
			if (_inputs == null)
			{
				enabled = false;
			}
		}

		private void Update()
		{
			HandleMovement();
			HandleLeaning();
		}

		private void HandleMovement()
		{
			_targetPosition = GetTargetPosition();
			_cachedTransform.position = Vector3.SmoothDamp(_cachedTransform.position, _targetPosition, ref _currentVelocity, _movementHandler.Smoothness);
		}

		private Vector3 GetTargetPosition()
		{
			var position = _targetPosition + new Vector3(_inputs.MovementInput.x, _inputs.MovementInput.y, 0) * (_movementHandler.Speed * Time.deltaTime);
			position.x = _cameraBounds.ClampX(position.x);
			position.y = _cameraBounds.ClampY(position.y);
			return position;
		}

		private void HandleLeaning()
		{
			var targetRotationAngle = -_inputs.MovementInput.x * _leaningHandler.LeanAngle;
			var currentRotation = _model.transform.localEulerAngles.y;
			var newRotation = Mathf.LerpAngle(currentRotation, targetRotationAngle, _leaningHandler.LeanSpeed * Time.deltaTime);
			_model.transform.localEulerAngles = new Vector3(0, newRotation, 0);
		}

	}

}