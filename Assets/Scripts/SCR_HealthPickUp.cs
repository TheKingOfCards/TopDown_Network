using Unity.Netcode;
using UnityEngine;

public class SCR_HealthPickUp : NetworkBehaviour
{
    [SerializeField] int healAmount;
    NetworkObject networkObj;

    private void Start()
    {
        networkObj = GetComponent<NetworkObject>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SCR_PlayerHealth pH = collider.GetComponent<SCR_PlayerHealth>();
            
            if(pH.Health != pH.MaxHealth)
            {
                pH.HealServerRpc(healAmount);
                DespawnObjServerRpc();
            }
        }
    }


    [ServerRpc]
    private void DespawnObjServerRpc()
    {
        networkObj.Despawn();
        Destroy(gameObject);
    }
}
