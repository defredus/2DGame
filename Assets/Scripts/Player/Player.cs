using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rb;

	private float _movementSpeed = 5f;
	private float _minMovingSpeed = 0.1f;
	private bool _isRunning = false;
	public static Player Instance { get; set; }

	private void Awake()
	{
		Instance = this;
		_rb = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		HandleMovement();
	}
	private void HandleMovement()
	{
		Vector2 inputVector = GameInput.Instance.GetMovementVector();
		_rb.MovePosition(_rb.position + inputVector * (_movementSpeed * Time.fixedDeltaTime));
		if (Mathf.Abs(inputVector.x) > _minMovingSpeed || Mathf.Abs(inputVector.y) > _minMovingSpeed)
		{
			_isRunning = true;
		}
		else { _isRunning = false; }
	}
	public bool IsRunning() => _isRunning;
	public Vector3 GetPlayerPosition() => Camera.main.WorldToScreenPoint(transform.position);
}

