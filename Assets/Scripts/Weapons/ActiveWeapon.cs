using UnityEngine;
using Scripts.Weapons.Sword_W;
using Scripts.Player_P;

namespace Scripts.Weapons
{
	public class ActiveWeapon : MonoBehaviour
	{
		public static ActiveWeapon Instance { get; private set; }

		[SerializeField] private Sword sword;

		private void Awake()
		{
			Instance = this;
		}
		private void Update()
		{
			if (Player.Instance.IsAlive())
			{
				FollowMousePosition();
			}
		}
		public Sword GetActiveWeapon()
		{
			return sword;
		}
		private void FollowMousePosition()
		{
			Vector3 mousePos = GameInput.Instance.GetMousePositiron();
			Vector3 playerPos = Player.Instance.GetPlayerPosition();
			if (mousePos.x < playerPos.x)
			{
				transform.rotation = Quaternion.Euler(0, 180, 0);
			}
			else
			{
				transform.rotation = Quaternion.Euler(0, 0, 0);
			}
		}
	}
}
