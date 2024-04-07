using UnityEngine;
using UnityEngine.InputSystem;
using Xomrac.Shmups.Utilities.Patterns;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;

namespace Xomrac.Shmups.Enteties.Player
{

	public class InputReader : ServiceComponent<EntityController>
	{
		[SerializeField] private PlayerInput _playerInput;
		[SerializeField] private InputActionReference _movementAction;
		[SerializeField] private InputActionReference _fireAction;
		

		public Vector2 MovementInput => _movementAction.action.ReadValue<Vector2>();
		public bool Fire => _fireAction.action.ReadValue<float>() > 0;

	}
}