using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour 
{
    [SerializeField] private float knockBackForce = 2f;
    [SerializeField] private float knockBackMovingTimerMax = 0.2f;

    private float _knockBackMovingTimer;

    private Rigidbody2D rb;

	public bool IsGettingKnockedBack { get; private set; }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_knockBackMovingTimer -= Time.deltaTime;
		if (_knockBackMovingTimer < 0) StopKnockBackMovement();
	}
	public void GetKnockedBack(Transform damageSource)
	{
		IsGettingKnockedBack = true;
		_knockBackMovingTimer = knockBackMovingTimerMax;
		Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce / rb.mass;
		rb.AddForce(difference, ForceMode2D.Impulse);
	}
	public void StopKnockBackMovement()
	{
		rb.linearVelocity = Vector2.zero;
		IsGettingKnockedBack = false;
	}
}
