using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour
{
	private PlayerInputActions _playerInputActions;
	public static GameInput Instance {  get; set; }

	public event EventHandler OnPlayerAttack;

	private void Awake()
	{
		Instance = this;
		_playerInputActions = new PlayerInputActions();
		_playerInputActions.Enable();
		_playerInputActions.Combat.Attack.started += PlayerAttack_started;
	}

	private void PlayerAttack_started(InputAction.CallbackContext obj)
	{
		OnPlayerAttack?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVector() => _playerInputActions.Player.Move.ReadValue<Vector2>();
	public Vector3 GetMousePositiron() => Mouse.current.position.ReadValue();
}
