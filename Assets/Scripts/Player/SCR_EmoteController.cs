using System.Threading;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_EmoteController : NetworkBehaviour
{
    [SerializeField] private Transform _emoteTransform;
    [SerializeField] private GameObject _smiley;
    [SerializeField] private float _emoteUpTime;
    [SerializeField] private float _emoteOffset;
    private float _timer;
    private GameObject _currentEmoteOLD;
    private bool _canEmote;
    private NetworkVariable<bool> _startEmote = new(false);
    private NetworkVariable<CurrentEmote> _currentEmote = new();


    private enum CurrentEmote
    {
        smiely
    } 

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        _timer = 0;
        _canEmote = true;

        _smiley.SetActive(false);
    }


    void Update()
    {
        if(_startEmote.Value)
        {
            EnableEmote();
            _startEmote.Value = false;
        }

        if(!IsOwner) return;

        if (_timer > -1) _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            if (!_canEmote)
            {
                if (IsOwner)
                {
                    DisableEmote();
                    _canEmote = true;
                }
            }
        }

        _emoteTransform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + _emoteOffset, transform.position.z), Quaternion.Euler(0, 0, 0));
    }


    private void OnEmote(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        if (_canEmote)
        {
            if (!IsOwner) return;

            if (input.y > 0)
            {
                _currentEmote.Value = CurrentEmote.smiely;
            }

            _startEmote.Value = true;
        }
    }


    private void EnableEmote()
    {
        if(_currentEmote.Value == CurrentEmote.smiely)
        {
            _smiley.SetActive(true);

            if(!IsOwner) return;

            _timer = _emoteUpTime;
            _canEmote = false;
        }
    }



    private void DisableEmote()
    {
        if(_currentEmote.Value == CurrentEmote.smiely) _smiley.SetActive(false);
    }
}
