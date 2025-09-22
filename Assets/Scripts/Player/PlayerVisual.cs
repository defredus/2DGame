using Scripts.Misc;
using System;
using UnityEngine;

namespace Scripts.Player_P
{
	public class PlayerVisual : MonoBehaviour
	{
		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		private FlashBlink _flashBlink;

		private const string IS_RUNNING = "IsRunning";
		private const string IS_DEAD = "IsDead";
		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_flashBlink = GetComponent<FlashBlink>();
		}
		private void Update()
		{
			_animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

			if (Player.Instance.IsAlive())
			{
				AdjustPlayerFacingDirection();
			}

		}
		private void Start()
		{
			Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
		}

		private void Player_OnPlayerDeath(object sender, EventArgs e)
		{
			_animator.SetBool(IS_DEAD, true);
			_flashBlink.StopBlinking();

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
		private void OnDestroy()
		{
			Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
		}
	}
}