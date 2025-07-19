using UnityEngine;


[RequireComponent (typeof(Animator))]
public class SkeletonVisual : MonoBehaviour
{
	[SerializeField] private EnemyAI _enemyAI;
	[SerializeField] private EnemyEntity _enemyEntity;

    private Animator _animator;

	private const string IS_RUNNING = "IsRunning";
	private const string CHASING_SPEED_MULTIPLIER = "ChaisingSpeedMultiplier";
	private const string ATTACK = "Attack";

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}
	private void Update()
	{
		_animator.SetBool(IS_RUNNING, _enemyAI.IsRunning);
		_animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
	}
	private void Start()
	{
		_enemyAI.onEnemyAttack += _enemyAI_OnEnemyAttack;
	}
	private void OnDestroy()
	{
		_enemyAI.onEnemyAttack -= _enemyAI_OnEnemyAttack;
	}
	public void AttackAnimationTriggerTurnOff()
	{
		_enemyEntity.PolygonColliderTurnOff();
	}
	public void AttackAnimationTriggerTurnOn()
	{
		_enemyEntity.PolygonColliderTurnOn();
	}
	private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
	{
		_animator.SetTrigger(ATTACK);
	}
}
