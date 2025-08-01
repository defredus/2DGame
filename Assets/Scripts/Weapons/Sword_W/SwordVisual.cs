using UnityEngine;

namespace Scripts.Weapons.Sword_W
{
	public class SwordVisual : MonoBehaviour
	{
		[SerializeField] private Sword sword;
		private Animator animator;
		private const string ATTACK = "Attack";
		private void Awake()
		{
			animator = GetComponent<Animator>();
		}
		private void Start()
		{
			sword.OnSwordSwing += Sword_OnSwordSwing;
		}
		public void TriggerEndAttackAnimation()
		{
			sword.AttackColliderTurnOff();
		}
		private void Sword_OnSwordSwing(object sender, System.EventArgs e)
		{
			animator.SetTrigger(ATTACK);
		}
		private void OnDestroy()
		{
			sword.OnSwordSwing -= Sword_OnSwordSwing;
		}
	}
}
