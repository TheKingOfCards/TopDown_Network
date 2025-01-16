using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_ClientRestrictions : NetworkBehaviour
{
    [SerializeField] SCR_PlayerController pC;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] SCR_PlayerHealth pH;


    private void Awake() // Set everything false here
    {
        pC.enabled = false;
        playerInput.enabled = false;
        pH.enabled = false;
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;

        if (!IsOwner) // Set false here
        {
            pC.enabled = false;
            playerInput.enabled = false;
            pH.enabled = false;
            enabled = false;
            return;
        }
        // Set true
        pC.enabled = true;
        playerInput.enabled = true;
        pH.enabled = true;
    }
}
