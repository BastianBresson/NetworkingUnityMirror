using Mirror;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class NetworkManagerLobby : NetworkManager
{
    [Scene] [SerializeField] private string menuScene = default;

    [SerializeField] private NetworkRoomPlayer roomPlayerPrefab = default;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefaps").ToList();
    }

    public override void OnStartClient()
    {
        GameObject[] spawnablePrefaps = Resources.LoadAll<GameObject>("SpawnablePrefaps");

        foreach (var prefab in spawnablePrefaps)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers > maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path != menuScene) { return; }

        NetworkRoomPlayer roomPlayer = Instantiate(roomPlayerPrefab);

        NetworkServer.AddPlayerForConnection(conn, roomPlayer.gameObject);
    }
}

