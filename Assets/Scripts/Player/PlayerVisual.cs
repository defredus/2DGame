using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private const string IS_RUNNING = "IsRunning";
	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void Update()
	{
		_animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
		AdjustPlayerFacingDirection();
	}

	private void AdjustPlayerFacingDirection()
	{
		Vector3 mousePos = GameInput.Instance.GetMousePositiron();
		Vector3 playerPos = Player.Instance.GetPlayerPosition();
		if (mousePos.x < playerPos.x)
		{
			_spriteRenderer.flipX = true;
		}
		else
		{
			_spriteRenderer.flipX = false;
		}
	}
}
