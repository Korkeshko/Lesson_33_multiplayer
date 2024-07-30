using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using Players;

namespace Projectiles.Bullet
{
    [RequireComponent(typeof(NetworkObject), typeof(NetworkTransform))]
    public class Bullet : NetworkBehaviour
    {
        private Vector2 direction;
        [SerializeField] private float speed = 10f;
        [SerializeField] private int damage = 1;

        public void Spawn(Vector3 dir)
        {
            direction = dir.normalized;
            GetComponent<NetworkObject>().Spawn(true);
        }

        private void Update()
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsServer)
            {
                if (other.gameObject.TryGetComponent<HP>(out HP hp))
                {
                    hp.TakeDamage(damage);
                } 
            }
        }
    }
}

