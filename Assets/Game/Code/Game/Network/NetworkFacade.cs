using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion.Sockets;
using UnityEngine;
using System;
using Fusion;
using Random = UnityEngine.Random;

namespace Game.Code.Game
{
    public class NetworkFacade : INetworkRunnerCallbacks
    {
        private readonly NetworkHostService _hostService;
        private readonly InputService _inputService;


        public NetworkFacade(InputService inputService, NetworkHostService hostService)
        {
            _inputService = inputService;
            _hostService = hostService;
        }


        public void OnInput(NetworkRunner runner, NetworkInput input) =>
            input.Set(_inputService.GetPlayerInput());

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            
            // TODO: Make network setup in Game scene, not in root state machine
            Debug.Log($"<color=white>Player joined</color>");
            var pos = Vector2.one * Random.value * 3f;
            _hostService.TryToSpawnPlayer(pos, player).Forget();
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) =>
            _hostService.TryToDespawnObject(player);

        #region [Unimplemented Callbacks]

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
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

        #endregion
    }
}