using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Scripts
{
	public class GameInput : MonoBehaviour
	{
		private PlayerInputActions _playerInputActions;
		public static GameInput Instance { get; set; }

		public event EventHandler OnPlayerAttack;
		public event EventHandler OnPlayerDash;

		private void Awake()
		{
			Instance = this;

			_playerInputActions = new PlayerInputActions();
			_playerInputActions.Enable();

			_playerInputActions.Combat.Attack.started += PlayerAttack_started;
			_playerInputActions.Player.Dash.performed += PlayerDash_performed;
		}

		public void DisableMovement()
		{
			_playerInputActions.Disable();
		}
		public Vector2 GetMovementVector() => _playerInputActions.Player.Move.ReadValue<Vector2>();
		public Vector3 GetMousePositiron() => Mouse.current.position.ReadValue();

		private void PlayerDash_performed(InputAction.CallbackContext context)
		{
			OnPlayerDash?.Invoke(this, EventArgs.Empty);
		}

		private void PlayerAttack_started(InputAction.CallbackContext obj)
		{
			OnPlayerAttack?.Invoke(this, EventArgs.Empty);
		}
	}
}
