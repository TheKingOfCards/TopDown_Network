using System.Threading;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_EmoteController : NetworkBehaviour
{
    [SerializeField] private GameObject _smiley;
    private bool _canEmote;


    private enum CurrentEmote
    {
        smiely
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _canEmote = true;
    }


    private void OnEmote(InputValue inputValue)
    {
        if (!IsOwner) return;

        Vector2 input = inputValue.Get<Vector2>();

        if (input.y > 0)
        {
            StartEmoteServerRpc();
        }
    }


    [ServerRpc]
    void StartEmoteServerRpc()
    {
        GameObject tempObj = Instantiate(_smiley);
        tempObj.GetComponent<SCR_Emote>()._followTransform = transform;
        tempObj.GetComponent<NetworkObject>().Spawn();
    }
}
