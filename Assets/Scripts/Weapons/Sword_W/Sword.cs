using Scripts.Skeleton;
using System;
using UnityEngine;

namespace Scripts.Weapons.Sword_W
{
	public class Sword : MonoBehaviour
	{
		[SerializeField] private int _damageAmount = 2;
		public event EventHandler OnSwordSwing;
		private PolygonCollider2D _polygonCollider2D;
		private void Start()
		{
			AttackColliderTurnOff();
		}
		private void Awake()
		{
			_polygonCollider2D = GetComponent<PolygonCollider2D>();
		}
		public void Attack()
		{
			AttackColliderTurnOffOn();
			OnSwordSwing?.Invoke(this, EventArgs.Empty);

		}
		public void AttackColliderTurnOff()
		{
			_polygonCollider2D.enabled = false;
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
			{
				enemyEntity.TakeDamage(_damageAmount);
			}
		}
		private void AttackColliderTurnOn()
		{
			_polygonCollider2D.enabled = true;
		}
		private void AttackColliderTurnOffOn()
		{
			AttackColliderTurnOff();
			AttackColliderTurnOn();
		}
	}
}
