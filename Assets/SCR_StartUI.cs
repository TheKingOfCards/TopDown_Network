using Unity.Netcode;
using UnityEngine;

public class SCR_StartUI : NetworkBehaviour
{
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        gameObject.SetActive(false);
    }


    public void Cilent()
    {
        NetworkManager.Singleton.StartClient();
        gameObject.SetActive(false);
    }
}
