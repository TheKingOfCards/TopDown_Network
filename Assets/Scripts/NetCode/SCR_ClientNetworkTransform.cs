using Unity.Netcode.Components;
using UnityEngine;

public class SCR_ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative() => false;
}
