using Unity.Netcode;
using UnityEngine;

public class SCR_BulletController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] public int damage;
    [SerializeField] float timeToDestroy = 3;
    float timer;

    NetworkObject networkObj;

    public override void OnNetworkSpawn()
    {
        timer = timeToDestroy;
        networkObj = GetComponent<NetworkObject>();
    }


    void Update()
    {
        if (!IsServer) return;

        transform.position += movementSpeed * Time.deltaTime * transform.up;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            DespawnBullet();
        }
    }


    private void DespawnBullet()
    {
        networkObj.Despawn();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!IsServer) return;

        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<SCR_PlayerHealth>().TakeDamage(damage);
            DespawnBullet();
        }

        if (collider.CompareTag("Wall"))
        {
            DespawnBullet();
        }
    }
}
