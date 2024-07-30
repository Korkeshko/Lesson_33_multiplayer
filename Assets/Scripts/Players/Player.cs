using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using Projectiles.Bullet;

[RequireComponent (typeof(Rigidbody2D), typeof(NetworkTransform))] 
public class Player : NetworkBehaviour
{
    #pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
    private Rigidbody2D rigidbody2D;
    #pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
    
    [SerializeField] private GameObject[] prefabBullet;
    [SerializeField] private Transform shootPoint;
    private KeyboardInput keyboardInput = new();
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 5f;
    private Quaternion lookAddition = Quaternion.Euler(0, -90, 0);
    [SerializeField] private NetworkVariable<float> inputDirection  = new(  0,
                                                                            NetworkVariableReadPermission.Everyone, 
                                                                            NetworkVariableWritePermission.Owner); 
    [SerializeField] private NetworkVariable<bool>  inputJump       = new(  false, 
                                                                            NetworkVariableReadPermission.Everyone, 
                                                                            NetworkVariableWritePermission.Owner);
    [SerializeField] private NetworkVariable<Color> color           = new(  Color.black,
                                                                            NetworkVariableReadPermission.Everyone, 
                                                                            NetworkVariableWritePermission.Owner);

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;  
    }

    // public override void OnNetworkSpawn()
    // {
    //     base.OnNetworkSpawn();
    //     color.Value = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    //     GetComponent<SpriteRenderer>().color = Color.black;
    // }

    private void Update()
    {
        if (IsOwner)
        {
            inputDirection.Value = keyboardInput.Direction();
            inputJump.Value = keyboardInput.Jump(); 
            
            if (keyboardInput.Shoot())
            {
               ShootServerRpc(); 
            }
             
        }

        if (IsServer)
        {
            Vector2 direction = Vector2.right * inputDirection.Value;
            if (direction.sqrMagnitude > 0f)
            {
                rigidbody2D.AddForce(direction * (speed * Time.deltaTime));
                transform.rotation = Quaternion.LookRotation(direction) * lookAddition;
            } 

            if (inputJump.Value)
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    } 

    [ServerRpc]
    private void ShootServerRpc()
    {
        int randomIndex = Random.Range(0, prefabBullet.Length);
        GameObject bullet = Instantiate(prefabBullet[randomIndex], shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Spawn(shootPoint.forward);
    }
}