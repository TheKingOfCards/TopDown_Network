using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEditor.PackageManager;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class SCR_PlayerHealth : NetworkBehaviour
{
    [SerializeField] private float _maxHealth;

    NetworkVariable<float> _health = new NetworkVariable<float>(0);
    private Slider _slider;

    // Getter
    public float Health
    {
        get
        {
            return _health.Value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        base.OnNetworkSpawn();

        _slider = SCR_GameManager.instance.healthSlider;
        SetUpHealthServerRpc();

        _health.OnValueChanged += UpdateUI;
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _health.OnValueChanged -= UpdateUI;
    }


    [ServerRpc]
    private void SetUpHealthServerRpc()
    {
        _health.Value = _maxHealth;
    }


    public void TakeDamage(int someDamage)
    {
        _health.Value -= someDamage;
    }


    [ServerRpc]
    public void HealServerRpc(int healAmount) // TODO -- Test if there is problem with ownership
    {
        if(!IsOwner) return;
        _health.Value += healAmount;
        if (_health.Value > _maxHealth) _health.Value = _maxHealth;
    }


    private void UpdateUI(float previousValue, float newValue)
    {
        if (!IsOwner) return;

        _slider.value = _health.Value / _maxHealth;
    }
}
