using UnityEngine;

namespace Scripts.Misc
{
	public class SelfDestroyVFX : MonoBehaviour
	{
		private ParticleSystem _ps;

		private void Awake()
		{
			_ps = GetComponent<ParticleSystem>();
		}
		private void Update()
		{
			if(_ps && !_ps.IsAlive())
			{
				DestroyItSelf();
			}
		}
		private void DestroyItSelf()
		{
			Destroy(gameObject);
		}
	}
}
