using System;
using UnityEngine;


[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonVisual : MonoBehaviour
{
	[SerializeField] private EnemyAI _enemyAI;
	[SerializeField] private EnemyEntity _enemyEntity;
	[SerializeField] private GameObject _enemyShadow;

    private Animator _animator;
	private SpriteRenderer _spriteRenderer;

	private const string IS_RUNNING = "IsRunning";
	private const string CHASING_SPEED_MULTIPLIER = "ChaisingSpeedMultiplier";
	private const string ATTACK = "Attack";
	private const string TAKE_HIT = "TakeHit";
	private const string IS_DEAD = "IsDead";

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void Update()
	{
		_animator.SetBool(IS_RUNNING, _enemyAI.IsRunning);
		_animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
	}
	private void Start()
	{
		_enemyAI.onEnemyAttack += _enemyAI_OnEnemyAttack;
		_enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
		_enemyEntity.OnDeath += _enemyEntity_OnDeath;
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
	private void _enemyEntity_OnDeath(object sender, EventArgs e)
	{
		_animator.SetBool(IS_DEAD, true);
		_spriteRenderer.sortingOrder = -1;
		_enemyShadow.SetActive(false);
	}

	private void _enemyEntity_OnTakeHit(object sender, EventArgs e)
	{
		_animator.SetTrigger(TAKE_HIT);
	}
	private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
	{
		_animator.SetTrigger(ATTACK);
	}
}
