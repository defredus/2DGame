using System;
using UnityEngine;
using Scripts.ScriptableObjects;
using Scripts.Player_P;

namespace Scripts.Skeleton
{
	[RequireComponent(typeof(PolygonCollider2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(EnemyAI))]
	public class EnemyEntity : MonoBehaviour
	{
		[SerializeField] private EnemySO _enemySO;

		private PolygonCollider2D _polygonCollider2D;
		private BoxCollider2D _boxCollider2D;
		private EnemyAI _enemyAI;

		private int _currentHealth;

		public event EventHandler OnTakeHit;
		public event EventHandler OnDeath;

		private void Start()
		{
			_currentHealth = _enemySO.enemyHealth;
			PolygonColliderTurnOff();

		}
		private void Awake()
		{
			_polygonCollider2D = GetComponent<PolygonCollider2D>();
			_boxCollider2D = GetComponent<BoxCollider2D>();
			_enemyAI = GetComponent<EnemyAI>();
		}
		private void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.transform.TryGetComponent(out Player player))
			{
				player.TakeDamage(transform, _enemySO.enemyDamageAmount);
			}
		}
		public void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			OnTakeHit?.Invoke(this, EventArgs.Empty);
			DetectDeath();
		}
		public void PolygonColliderTurnOff()
		{
			//Debug.Log("ColliderOff");
			_polygonCollider2D.enabled = false;
		}
		public void PolygonColliderTurnOn()
		{
			//Debug.Log("ColliderOn");
			_polygonCollider2D.enabled = true;
		}
		private void DetectDeath()
		{
			if (_currentHealth <= 0)
			{
				_boxCollider2D.enabled = false;
				_polygonCollider2D.enabled = false;
				_enemyAI.SetDeathState();


				OnDeath?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
