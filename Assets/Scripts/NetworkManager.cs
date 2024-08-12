using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour,INetworkRunnerCallbacks
{
    private NetworkRunner networkRunner;

    private void Start()
    {
        // Initialize the NetworkRunner
        networkRunner = transform.AddComponent<NetworkRunner>();
    }

    public void CreateRoom(string roomName)
    {
        StartGame(GameMode.Host, roomName);
    }

    public void JoinRoom(string roomName)
    {
        StartGame(GameMode.Client, roomName);
    }

    private void StartGame(GameMode gameMode, string roomName)
    {
        networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = roomName,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        }).ContinueWith(task => {
            if (task.IsCompleted)
            {
                OnGameStarted();
            }
        });
    }
    private void HandlePlayerJoined()
    {
        // Check if there are more than 1 player
        if (networkRunner.ActivePlayers.ToList().Count > 1)
        {
            UIManager.Instance.ShowOnGameUI();
        }
    }
    private void OnGameStarted()
    {
        UIManager.Instance.ShowWaitingPanel();
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("playerJoined");
        HandlePlayerJoined();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {

    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
}
