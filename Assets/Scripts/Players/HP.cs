using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class HP : NetworkBehaviour
    {
        [SerializeField] private float countHP;
        public UnityEvent OnDead = new();
        public bool Alive => countHP > 0f;
    
        public void TakeDamage(float damage)
        {
            countHP -= damage;
            if (countHP <= 0f)
            {
                OnDead.Invoke();
                gameObject.SetActive(false);
                GetComponent<NetworkObject>().Despawn(true);
            }
        }
    }
}
