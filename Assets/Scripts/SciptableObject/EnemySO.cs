using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu()]
    public class EnemySO : ScriptableObject
    {
        public string enemyName;
        public int enemyHealth;
        public int enemyDamageAmount;
    }
}
