using Fusion;
using UnityEngine;
using UnityEngine.UI;


public struct NetworkInputData : INetworkInput
{
    public Vector3 moveDirection;
    public NetworkBool isKicking;
}