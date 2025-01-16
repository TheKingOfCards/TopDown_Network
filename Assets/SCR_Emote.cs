using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_Emote : NetworkBehaviour
{
    [HideInInspector] public Transform _followTransform;
    private NetworkObject _networkObject;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _emoteUpTime;
    private float _timer;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _timer = _emoteUpTime;
        _networkObject = GetComponent<NetworkObject>();
    }


    private void Update()
    {
        if(!IsOwner) return;

        Debug.Log(OwnerClientId);

        transform.SetPositionAndRotation(new(_followTransform.position.x, _followTransform.position.y + _yOffset, _followTransform.position.z), new(0, 0, 0, 0));

        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            DisableEmoteServerRpc();
        }
    }


    [ServerRpc]
    void DisableEmoteServerRpc()
    {
        _networkObject.Despawn(true);
    }
}
