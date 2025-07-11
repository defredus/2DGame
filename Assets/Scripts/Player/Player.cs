using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rb;

	private float _movementSpeed = 5f;
	private float _minMovingSpeed = 0.1f;
	private bool _isRunning = false;
	Vector2 inputVector;
	public static Player Instance { get; set; }

	private void Start()
	{
		GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
	}
	private void Awake()
	{
		Instance = this;
		_rb = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		HandleMovement();
	}
	private void Update()
	{
		inputVector = GameInput.Instance.GetMovementVector();
		
	}
	private void Player_OnPlayerAttack(object sender, System.EventArgs e)
	{
		ActiveWeapon.Instance.GetActiveWeapon().Attack();
	}
	private void HandleMovement()
	{
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

