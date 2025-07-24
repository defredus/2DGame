using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(KnockBack))]
public class Player : MonoBehaviour
{

	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private float _movementSpeed = 5f;
	[SerializeField] private float _damageRecoveryTime = 0.5f;

	private Rigidbody2D _rb;
	private KnockBack _knockBack;

	public event EventHandler OnPlayerDeath;
	public event EventHandler OnFlsahBlink;
	public static Player Instance { get; set; }

	private float _minMovingSpeed = 0.1f;
	private bool _isRunning = false;
	Vector2 inputVector;
	private int _currentHealth;
	private bool _canTakeDamage;
	private bool _isAlive;
	private void Start()
	{
		_canTakeDamage = true;
		_currentHealth = _maxHealth;
		_isAlive = true;
		GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
	}
	private void Awake()
	{
		Instance = this;
		_rb = GetComponent<Rigidbody2D>();
		_knockBack = GetComponent<KnockBack>();
	}
	private void FixedUpdate()
	{
		if (_knockBack.IsGettingKnockedBack) return;
		HandleMovement();
	}
	private void Update()
	{
		inputVector = GameInput.Instance.GetMovementVector();
		
	}
	public bool IsAlive() => _isAlive;
	public bool IsRunning() => _isRunning;
	public Vector3 GetPlayerPosition() => Camera.main.WorldToScreenPoint(transform.position);
	public void TakeDamage(Transform dagameSource, int damage)
	{
		if (_canTakeDamage && _isAlive)
		{
			_canTakeDamage = false;
			_currentHealth = Mathf.Max(0, _currentHealth - damage);
			_knockBack.GetKnockedBack(dagameSource);

			OnFlsahBlink?.Invoke(this, EventArgs.Empty);

			StartCoroutine(DamageRecoveryRoutine());
		}
		DetectDeath();
	}
	private IEnumerator DamageRecoveryRoutine()
	{
		yield return new WaitForSeconds(_damageRecoveryTime);
		_canTakeDamage = true;
	}
	private void DetectDeath()
	{
		if(_currentHealth == 0 && _isAlive)
		{
			_isAlive = false;
			_knockBack.StopKnockBackMovement();
			GameInput.Instance.DisableMovement();

			OnPlayerDeath?.Invoke(this, EventArgs.Empty);
		}
	}
	private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
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
	private void OnDestroy()
	{
		GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
	}
}

