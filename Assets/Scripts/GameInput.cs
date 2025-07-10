using UnityEngine;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour
{
	private PlayerInputActions _playerInputActions;
	public static GameInput Instance {  get; set; }

	private void Awake()
	{
		Instance = this;
		_playerInputActions = new PlayerInputActions();
		_playerInputActions.Enable();
	}
	public Vector2 GetMovementVector() => _playerInputActions.Player.Move.ReadValue<Vector2>();
	public Vector3 GetMousePositiron() => Mouse.current.position.ReadValue();
}
