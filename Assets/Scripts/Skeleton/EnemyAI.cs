using UnityEngine;
using UnityEngine.AI;
using System;
using Scripts.Player_P;
using Scripts.Utils;

namespace Scripts.Skeleton
{
    public class EnemyAI : MonoBehaviour
    {
	    [SerializeField] private State startingState;
	    [SerializeField] private float roamingDistanceMax = 7f;
        [SerializeField] private float roamingDistanceMin = 3f;
        [SerializeField] private float roamingTimerMax = 2f;

        [SerializeField] private bool isChaisngEnemy = false;
	    [SerializeField]  private float chasingDistance = 4f;
	    [SerializeField]  private float chasingSpeedMultiplier = 2f;

	    [SerializeField] private bool isAttackingEnemy = false;
	    [SerializeField] private float attackingDistance = 2f;
	    [SerializeField] private float attackRate = 2f;
        private float _nextAttackTime = 0f;


	    private NavMeshAgent _navMeshAgent;
        private State _currentState;
        private float _roamingTimer;
        private Vector3 _roamPosition;
        private Vector3 _startingPosition;

        private float _roamingSpeed;
        private float _chasingSpeed;

        private float _nextCheckDirectionTime = 0f;
        private float _checkDirectionDuration = 0.1f;
        private Vector3 _lastPosition;

        public event EventHandler onEnemyAttack;
        public bool IsRunning {
            get {
                if (_navMeshAgent.velocity == Vector3.zero) return false;
                else return true;
            }
        }
        private enum State
        {
            Idle,
            Roaming,
            Chasing,
            Attacking,
            Death
        }

	    private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            _currentState = startingState;
            _roamingSpeed = _navMeshAgent.speed;
            _chasingSpeed = _navMeshAgent.speed * chasingSpeedMultiplier;

        }
	    private void Update()
	    {
            StateHandler();
            MovementDirectionHandler();
	    }
        public void SetDeathState()
        {
            _navMeshAgent.ResetPath();
            _currentState = State.Death;
        }
        public float GetRoamingAnimationSpeed()
        {
            return _navMeshAgent.speed / _roamingSpeed;
        }
        private void StateHandler()
        {
		    switch (_currentState)
		    {
			    case State.Roaming:
				    _roamingTimer -= Time.deltaTime;
				    if (_roamingTimer < 0)
				    {
					    Roaming();
					    _roamingTimer = roamingTimerMax;
				    }
                    CheckCurrentState();
				    break;
			    case State.Idle:
				    break;
			    case State.Chasing:
				    ChasingTarget();
                    CheckCurrentState();
				    break;
			    case State.Death:
				    break;
			    case State.Attacking:
                    AttackingTarget();
				    CheckCurrentState();
				    break;
		    }
	    }
        private State CheckCurrentState()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
            State newState = State.Roaming;
            if (isChaisngEnemy)
            {
                if (Player.Instance.IsAlive() && distanceToPlayer <= chasingDistance)
                {
                    newState = State.Chasing;
                }
            }

            if (isAttackingEnemy)
            {
                if (distanceToPlayer <= attackingDistance)
                {
                    if (Player.Instance.IsAlive()) { newState = State.Attacking; }
                    else { newState = State.Roaming; }
                }
            }

            if (newState != _currentState)
            {
                if (newState == State.Chasing)
                {
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.speed = _chasingSpeed;
                }
                else if (newState == State.Roaming)
                {
                    _roamingTimer = 0f;
                    _navMeshAgent.speed = _roamingSpeed;
                }
                else if (newState == State.Attacking)
                { _navMeshAgent.ResetPath(); }
                _currentState = newState;
            }
            return newState;
        }
        private void ChasingTarget()
        {
            _navMeshAgent.SetDestination(Player.Instance.transform.position);
        }
        private void AttackingTarget()
        {
            if(Time.time > _nextAttackTime)
            {
			    onEnemyAttack?.Invoke(this, EventArgs.Empty);
                _nextAttackTime = Time.time + attackRate;
		    }
        
        }
	    private void Roaming()
        {
            _startingPosition = transform.position;
            _roamPosition = GetRoamingPosition();
            _navMeshAgent.SetDestination(_roamPosition);
        }
	    private Vector3 GetRoamingPosition()
        {
            return _startingPosition + Util.GetRandomDir() * 
                UnityEngine.Random.Range(
                    roamingDistanceMin, 
                    roamingDistanceMax
                    );
        }
        private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
        {
            if(sourcePosition.x > targetPosition.x) { transform.rotation = Quaternion.Euler(0, -180, 0); }
            else { transform.rotation = Quaternion.Euler(0, 0, 0);  }
        }
        private void MovementDirectionHandler()
        {
            if(Time.time > _nextCheckDirectionTime)
            {
                if (IsRunning)
                {
                    ChangeFacingDirection(_lastPosition, transform.position);
                } else if(_currentState == State.Attacking)
                {
                    ChangeFacingDirection(transform.position, Player.Instance.transform.position);
                }
                _lastPosition = transform.position;
                _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
            }
        }
	}
}
