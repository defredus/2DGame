using UnityEngine;

public class SkeletonVisual : MonoBehaviour
{
	[SerializeField] private EnemyAI _enemyAI;
    private Animator _animator;
	private const string IS_RUNNING = "IsRunning";
	private const string CHASING_SPEED_MULTIPLIER = "ChaisingSpeedMultyplier";

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}
	private void Update()
	{
		_animator.SetBool(IS_RUNNING, _enemyAI.IsRunning);
		_animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
	}
}
